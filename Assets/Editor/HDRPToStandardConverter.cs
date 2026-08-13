// HDRPToStandardConverter.cs
// Put this file in:  Assets/Editor/HDRPToStandardConverter.cs
// Open with:         Tools > HDRP -> Standard Converter
//
// Converts materials that were upgraded to HDRP shaders back to the
// Built-in Render Pipeline's Standard shader.
//
// It reads material values through SerializedObject rather than the
// Material API, so it still works after the HDRP package has been removed
// and the shaders no longer resolve.
//
// ALWAYS run in "Report only" mode first, and back up the project before
// running a real conversion.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class HDRPToStandardConverter : EditorWindow
{
    private bool reportOnly = true;
    private bool convertUnlit = true;
    private bool convertTerrain = true;
    private float emissionScale = 1f;
    private Vector2 scroll;
    private string lastReport = "";

    [MenuItem("Tools/HDRP -> Standard Converter")]
    public static void Open()
    {
        GetWindow<HDRPToStandardConverter>("HDRP -> Standard");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("HDRP -> Built-in Standard", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(
            "Back up your project before running a real conversion.\n" +
            "This rewrites .mat files and cannot be undone from the Editor.",
            MessageType.Warning);

        EditorGUILayout.Space();
        reportOnly = EditorGUILayout.ToggleLeft(
            "Report only (make no changes)", reportOnly);
        convertUnlit = EditorGUILayout.ToggleLeft(
            "Also convert HDRP/Unlit -> Unlit", convertUnlit);
        convertTerrain = EditorGUILayout.ToggleLeft(
            "Also convert HDRP/TerrainLit -> Nature/Terrain/Standard", convertTerrain);
        emissionScale = EditorGUILayout.Slider(
            "Emission scale", emissionScale, 0f, 2f);
        EditorGUILayout.HelpBox(
            "HDRP emission uses physical units and is often far brighter than " +
            "Standard expects. If emissive materials look blown out, lower this " +
            "and run again on a fresh copy.",
            MessageType.Info);

        EditorGUILayout.Space();
        GUI.backgroundColor = reportOnly ? Color.white : new Color(1f, 0.6f, 0.6f);
        if (GUILayout.Button(reportOnly ? "Scan Materials" : "CONVERT MATERIALS",
                             GUILayout.Height(32)))
        {
            Run();
        }
        GUI.backgroundColor = Color.white;

        if (!string.IsNullOrEmpty(lastReport))
        {
            EditorGUILayout.Space();
            scroll = EditorGUILayout.BeginScrollView(scroll);
            EditorGUILayout.TextArea(lastReport, GUILayout.ExpandHeight(true));
            EditorGUILayout.EndScrollView();
        }
    }

    // ---------------------------------------------------------------- run

    private void Run()
    {
        var guids = AssetDatabase.FindAssets("t:Material");
        var log = new StringBuilder();
        int converted = 0, skipped = 0, failed = 0;

        try
        {
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);

                // Never touch materials that live inside packages.
                if (path.StartsWith("Packages/")) continue;

                if (EditorUtility.DisplayCancelableProgressBar(
                        "HDRP -> Standard",
                        path,
                        (float)i / Mathf.Max(1, guids.Length)))
                    break;

                var mat = AssetDatabase.LoadAssetAtPath<Material>(path);
                if (mat == null) continue;

                string shaderName = ReadShaderName(mat);
                var target = PickTargetShader(shaderName);

                if (target == null) { skipped++; continue; }

                if (reportOnly)
                {
                    log.AppendLine($"WOULD CONVERT  [{shaderName}] -> [{target}]  {path}");
                    converted++;
                    continue;
                }

                if (Convert(mat, target, log, path)) converted++;
                else failed++;
            }
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }

        if (!reportOnly)
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        string head =
            $"{(reportOnly ? "SCAN" : "CONVERSION")} COMPLETE\n" +
            $"Materials affected : {converted}\n" +
            $"Left alone         : {skipped}\n" +
            $"Failed             : {failed}\n" +
            new string('-', 60) + "\n";

        lastReport = head + log;
        Debug.Log(lastReport);

        string reportPath = Path.Combine(
            Application.dataPath, "..", "hdrp_to_standard_report.txt");
        File.WriteAllText(reportPath, lastReport);
        Debug.Log("Report written to: " + Path.GetFullPath(reportPath));
    }

    // Returns null when the material should be left alone.
    private string PickTargetShader(string shaderName)
    {
        if (string.IsNullOrEmpty(shaderName)) return null;
        if (!shaderName.StartsWith("HDRP/")) return null;

        if (shaderName.Contains("TerrainLit"))
            return convertTerrain ? "Nature/Terrain/Standard" : null;

        if (shaderName.Contains("Unlit"))
            return convertUnlit ? "Unlit/Texture" : null;

        // Lit, LitTessellation, LayeredLit, ... all map to Standard.
        return "Standard";
    }

    // ------------------------------------------------------------ convert

    private bool Convert(Material mat, string targetShaderName,
                         StringBuilder log, string path)
    {
        var shader = Shader.Find(targetShaderName);
        if (shader == null)
        {
            log.AppendLine($"FAILED  shader '{targetShaderName}' not found  {path}");
            return false;
        }

        // Read every saved value BEFORE swapping the shader.
        var tex = ReadTextures(mat);
        var flt = ReadFloats(mat);
        var col = ReadColors(mat);

        Texture Tex(string n) => tex.TryGetValue(n, out var v) ? v.tex : null;
        Vector2 Scale(string n) => tex.TryGetValue(n, out var v) ? v.scale : Vector2.one;
        Vector2 Offset(string n) => tex.TryGetValue(n, out var v) ? v.offset : Vector2.zero;
        float Flt(string n, float d = 0f) => flt.TryGetValue(n, out var v) ? v : d;
        Color Col(string n, Color d) => col.TryGetValue(n, out var v) ? v : d;

        var baseMap = Tex("_BaseColorMap");
        var maskMap = Tex("_MaskMap");
        var normalMap = Tex("_NormalMap");
        var emissiveMap = Tex("_EmissiveColorMap");
        var heightMap = Tex("_HeightMap");

        var baseColor = Col("_BaseColor", Color.white);
        float metallic = Flt("_Metallic", 0f);
        float smoothness = Flt("_Smoothness", 0.5f);
        float normalScale = Flt("_NormalScale", 1f);
        float surfaceType = Flt("_SurfaceType", 0f);      // 0 opaque, 1 transparent
        float cutoffEnable = Flt("_AlphaCutoffEnable", 0f);
        float cutoff = Flt("_AlphaCutoff", 0.5f);
        float heightAmp = Flt("_HeightAmplitude", 0.02f);

        // HDRP stores emission either as a final HDR colour, or as an LDR
        // colour plus a separate intensity multiplier.
        Color emissive;
        if (Flt("_UseEmissiveIntensity", 0f) > 0.5f)
            emissive = Col("_EmissiveColorLDR", Color.black) * Flt("_EmissiveIntensity", 1f);
        else
            emissive = Col("_EmissiveColor", Color.black);

        // ---- swap the shader, then write the Standard values -------------
        Undo.RecordObject(mat, "Convert HDRP material to Standard");
        mat.shader = shader;

        if (targetShaderName == "Standard")
        {
            mat.SetColor("_Color", baseColor);
            if (baseMap) mat.SetTexture("_MainTex", baseMap);
            mat.SetTextureScale("_MainTex", Scale("_BaseColorMap"));
            mat.SetTextureOffset("_MainTex", Offset("_BaseColorMap"));

            // HDRP mask map:  R metallic, G occlusion, B detail, A smoothness
            // Standard _MetallicGlossMap reads R + A, _OcclusionMap reads G.
            // The channels line up, so the same texture serves both slots.
            if (maskMap)
            {
                mat.SetTexture("_MetallicGlossMap", maskMap);
                mat.SetTexture("_OcclusionMap", maskMap);
                mat.EnableKeyword("_METALLICGLOSSMAP");
                mat.SetFloat("_GlossMapScale", smoothness);
                mat.SetFloat("_OcclusionStrength", 1f);
            }
            else
            {
                mat.SetFloat("_Metallic", metallic);
                mat.SetFloat("_Glossiness", smoothness);
                mat.DisableKeyword("_METALLICGLOSSMAP");
            }

            if (normalMap)
            {
                mat.SetTexture("_BumpMap", normalMap);
                mat.SetFloat("_BumpScale", normalScale);
                mat.EnableKeyword("_NORMALMAP");
            }

            if (heightMap)
            {
                mat.SetTexture("_ParallaxMap", heightMap);
                mat.SetFloat("_Parallax", Mathf.Clamp(heightAmp, 0.005f, 0.08f));
                mat.EnableKeyword("_PARALLAXMAP");
            }

            bool hasEmission = emissiveMap != null || emissive.maxColorComponent > 0.001f;
            if (hasEmission)
            {
                if (emissiveMap) mat.SetTexture("_EmissionMap", emissiveMap);
                mat.SetColor("_EmissionColor", emissive * emissionScale);
                mat.EnableKeyword("_EMISSION");
                mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
            }
            else
            {
                mat.DisableKeyword("_EMISSION");
                mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
            }

            mat.SetFloat("_Cutoff", cutoff);

            StandardMode mode;
            if (surfaceType > 0.5f) mode = StandardMode.Fade;
            else if (cutoffEnable > 0.5f) mode = StandardMode.Cutout;
            else mode = StandardMode.Opaque;
            SetupStandardBlendMode(mat, mode);

            log.AppendLine($"OK  -> Standard ({mode})  {path}");
        }
        else if (targetShaderName == "Unlit/Texture")
        {
            if (baseMap) mat.SetTexture("_MainTex", baseMap);
            log.AppendLine($"OK  -> Unlit/Texture  {path}");
        }
        else // terrain
        {
            log.AppendLine($"OK  -> {targetShaderName} (check terrain layers by hand)  {path}");
        }

        EditorUtility.SetDirty(mat);
        return true;
    }

    // ------------------------------------------------- serialized reading

    private struct TexEntry
    {
        public Texture tex;
        public Vector2 scale;
        public Vector2 offset;
    }

    // Reads the shader name straight from the asset, so it still reports
    // correctly when the HDRP shaders are missing from the project.
    private static string ReadShaderName(Material mat)
    {
        if (mat.shader != null &&
            mat.shader.name != "Hidden/InternalErrorShader")
            return mat.shader.name;

        // Shader is gone. Recover the name from the referenced shader asset.
        var so = new SerializedObject(mat);
        var shaderProp = so.FindProperty("m_Shader");
        if (shaderProp == null) return null;

        string path = AssetDatabase.GetAssetPath(shaderProp.objectReferenceValue);
        if (string.IsNullOrEmpty(path)) return null;

        var s = AssetDatabase.LoadAssetAtPath<Shader>(path);
        return s != null ? s.name : null;
    }

    private static Dictionary<string, TexEntry> ReadTextures(Material mat)
    {
        var result = new Dictionary<string, TexEntry>();
        var so = new SerializedObject(mat);
        var arr = so.FindProperty("m_SavedProperties.m_TexEnvs");
        if (arr == null) return result;

        for (int i = 0; i < arr.arraySize; i++)
        {
            var el = arr.GetArrayElementAtIndex(i);
            string key = el.FindPropertyRelative("first").stringValue;
            var val = el.FindPropertyRelative("second");
            if (string.IsNullOrEmpty(key) || val == null) continue;

            result[key] = new TexEntry
            {
                tex = val.FindPropertyRelative("m_Texture").objectReferenceValue as Texture,
                scale = val.FindPropertyRelative("m_Scale").vector2Value,
                offset = val.FindPropertyRelative("m_Offset").vector2Value,
            };
        }
        return result;
    }

    private static Dictionary<string, float> ReadFloats(Material mat)
    {
        var result = new Dictionary<string, float>();
        var so = new SerializedObject(mat);
        var arr = so.FindProperty("m_SavedProperties.m_Floats");
        if (arr == null) return result;

        for (int i = 0; i < arr.arraySize; i++)
        {
            var el = arr.GetArrayElementAtIndex(i);
            string key = el.FindPropertyRelative("first").stringValue;
            if (string.IsNullOrEmpty(key)) continue;
            result[key] = el.FindPropertyRelative("second").floatValue;
        }
        return result;
    }

    private static Dictionary<string, Color> ReadColors(Material mat)
    {
        var result = new Dictionary<string, Color>();
        var so = new SerializedObject(mat);
        var arr = so.FindProperty("m_SavedProperties.m_Colors");
        if (arr == null) return result;

        for (int i = 0; i < arr.arraySize; i++)
        {
            var el = arr.GetArrayElementAtIndex(i);
            string key = el.FindPropertyRelative("first").stringValue;
            if (string.IsNullOrEmpty(key)) continue;
            result[key] = el.FindPropertyRelative("second").colorValue;
        }
        return result;
    }

    // ---------------------------------------------------- standard blending

    private enum StandardMode { Opaque = 0, Cutout = 1, Fade = 2, Transparent = 3 }

    private static void SetupStandardBlendMode(Material m, StandardMode mode)
    {
        m.SetFloat("_Mode", (float)mode);

        switch (mode)
        {
            case StandardMode.Opaque:
                m.SetInt("_SrcBlend", (int)BlendMode.One);
                m.SetInt("_DstBlend", (int)BlendMode.Zero);
                m.SetInt("_ZWrite", 1);
                m.DisableKeyword("_ALPHATEST_ON");
                m.DisableKeyword("_ALPHABLEND_ON");
                m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                m.renderQueue = -1;
                break;

            case StandardMode.Cutout:
                m.SetInt("_SrcBlend", (int)BlendMode.One);
                m.SetInt("_DstBlend", (int)BlendMode.Zero);
                m.SetInt("_ZWrite", 1);
                m.EnableKeyword("_ALPHATEST_ON");
                m.DisableKeyword("_ALPHABLEND_ON");
                m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                m.renderQueue = (int)RenderQueue.AlphaTest;
                break;

            case StandardMode.Fade:
                m.SetInt("_SrcBlend", (int)BlendMode.SrcAlpha);
                m.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
                m.SetInt("_ZWrite", 0);
                m.DisableKeyword("_ALPHATEST_ON");
                m.EnableKeyword("_ALPHABLEND_ON");
                m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                m.renderQueue = (int)RenderQueue.Transparent;
                break;

            case StandardMode.Transparent:
                m.SetInt("_SrcBlend", (int)BlendMode.One);
                m.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
                m.SetInt("_ZWrite", 0);
                m.DisableKeyword("_ALPHATEST_ON");
                m.DisableKeyword("_ALPHABLEND_ON");
                m.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                m.renderQueue = (int)RenderQueue.Transparent;
                break;
        }
    }
}
