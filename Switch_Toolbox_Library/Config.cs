﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.Drawing;
using System.IO;

namespace Switch_Toolbox.Library
{
    //Based on
    // https://github.com/jam1garner/Smash-Forge/blob/26e0dcbd84cdf8a4ffe3fbe0b0317520a4099286/Smash%20Forge/Filetypes/Application/Config.cs
    public class Config
    {
        public static void StartupFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Save();
                return;
            }

            ReadConfigFromFile(fileName);
        }

        public static void GamePathsFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                SavePaths();
                return;
            }

            ReadConfigFromFile(fileName);
        }

        private static void ReadConfigFromFile(string fileName)
        {
            int discordImageKey;
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            Queue<XmlNode> que = new Queue<XmlNode>();

            foreach (XmlNode node in doc.ChildNodes)
                que.Enqueue(node);

            while (que.Count > 0)
            {
                XmlNode node = que.Dequeue();

                foreach (XmlNode n in node.ChildNodes)
                    que.Enqueue(n);

                switch (node.Name)
                {
                    case "UseDebugDomainExceptionHandler":
                        bool.TryParse(node.InnerText, out Runtime.UseDebugDomainExceptionHandler);
                        break;
                    case "OpenStartupWindow":
                        bool.TryParse(node.InnerText, out Runtime.OpenStartupWindow);
                        break;
                    case "EnableVersionCheck":
                        bool.TryParse(node.InnerText, out Runtime.EnableVersionCheck);
                        break;
                    case "RenderModels":
                        bool.TryParse(node.InnerText, out Runtime.RenderModels);
                        break;
                    case "RenderModelSelection":
                        bool.TryParse(node.InnerText, out Runtime.RenderModelSelection);
                        break;
                    case "RenderModelWireframe":
                        bool.TryParse(node.InnerText, out Runtime.RenderModelWireframe);
                        break;
                    case "EnablePBR":
                        bool.TryParse(node.InnerText, out Runtime.EnablePBR);
                        break;
                    case "viewportShading":
                        if (node.ParentNode != null && node.ParentNode.Name.Equals("RENDERSETTINGS"))
                            Enum.TryParse(node.InnerText, out Runtime.viewportShading);
                        break;
                    case "thumbnailSize":
                        if (node.ParentNode != null && node.ParentNode.Name.Equals("OBJLISTSETTINGS"))
                            Enum.TryParse(node.InnerText, out Runtime.thumbnailSize);
                        break;
                    case "stereoscopy": bool.TryParse(node.InnerText, out Runtime.stereoscopy);
                        break;
                    case "CameraFar":
                        float.TryParse(node.InnerText, out Runtime.CameraFar);
                        break;
                    case "CameraNear":
                        float.TryParse(node.InnerText, out Runtime.CameraNear);
                        break;
                    case "PreviewScale":
                        float.TryParse(node.InnerText, out Runtime.previewScale);
                        break;
                    case "Yaz0CompressionLevel":
                        int.TryParse(node.InnerText, out Runtime.Yaz0CompressionLevel);
                        break;
                    case "FormTheme":
                        Switch_Toolbox.Library.Forms.FormThemes.Preset preset;
                        Enum.TryParse(node.InnerText, out preset);
                        Switch_Toolbox.Library.Forms.FormThemes.ActivePreset = preset;
                        break;
                    case "MaximizeMdiWindow": bool.TryParse(node.InnerText, out Runtime.MaximizeMdiWindow);
                        break;
                    case "PreferredTexFormat":
                        TEX_FORMAT texFormat;
                        Enum.TryParse(node.InnerText, out texFormat);
                        Runtime.PreferredTexFormat = texFormat;
                        break;
                    case "backgroundGradientTop":
                        TryParseHexColor(node, ref Runtime.backgroundGradientTop);
                        break;
                    case "backgroundGradientBottom":
                        TryParseHexColor(node, ref Runtime.backgroundGradientBottom);
                        break;
                    case "renderVertColor":
                        bool.TryParse(node.InnerText, out Runtime.renderVertColor);
                        break;
                    case "renderReflection":
                        bool.TryParse(node.InnerText, out Runtime.renderReflection);
                        break;
                    case "renderSpecular":
                        bool.TryParse(node.InnerText, out Runtime.renderSpecular);
                        break;
                    case "useNormalMap":
                        bool.TryParse(node.InnerText, out Runtime.useNormalMap);
                        break;
                    case "CellSize":
                        float.TryParse(node.InnerText, out Runtime.gridSettings.CellSize);
                        break;
                    case "CellAmount":
                        uint.TryParse(node.InnerText, out Runtime.gridSettings.CellAmount);
                        break;
                    case "GridColor":
                        TryParseHexColor(node, ref Runtime.gridSettings.color);
                        break;
                    case "ListPanelWidth":
                        int.TryParse(node.InnerText, out Runtime.ObjectEditor.ListPanelWidth);
                        break;
                    case "ViewportWidth":
                        int.TryParse(node.InnerText, out Runtime.ViewportEditor.Width);
                        break;
                    case "renderBones":
                        bool.TryParse(node.InnerText, out Runtime.renderBones);
                        break;
                    case "normalsLineLength":
                        float.TryParse(node.InnerText, out Runtime.normalsLineLength);
                        break;
                    case "bonePointSize":
                        float.TryParse(node.InnerText, out Runtime.bonePointSize);
                        break;
                    case "MaxCameraSpeed":
                        float.TryParse(node.InnerText, out Runtime.MaxCameraSpeed);
                        break;
                    case "AddFilesToActiveObjectEditor":
                        bool.TryParse(node.InnerText, out Runtime.AddFilesToActiveObjectEditor);
                        break;
                    case "SmoGamePath":
                        Runtime.SmoGamePath = node.InnerText;
                        break;
                    case "Mk8GamePath":
                        Runtime.Mk8GamePath = node.InnerText;
                        break;
                    case "Mk8dGamePath":
                        Runtime.Mk8dGamePath = node.InnerText;
                        break;
                    case "TpGamePath":
                        Runtime.TpGamePath = node.InnerText;
                        break;
                    case "BotwGamePath":
                        Runtime.BotwGamePath = node.InnerText;
                        break;
                    case "renderBoundingBoxes":
                        bool.TryParse(node.InnerText, out Runtime.renderBoundingBoxes);
                        break;
                    case "UseOpenGL":
                        bool.TryParse(node.InnerText, out Runtime.UseOpenGL);
                        break;
                    case "DisplayViewport":
                        bool.TryParse(node.InnerText, out Runtime.DisplayViewport);
                        break;
                    case "DisplayVertical":
                        bool.TryParse(node.InnerText, out Runtime.ImageEditor.DisplayVertical);
                        break;
                    case "ShowPropertiesPanel":
                        bool.TryParse(node.InnerText, out Runtime.ImageEditor.ShowPropertiesPanel);
                        break;
                    case "DisplayAlpha":
                        bool.TryParse(node.InnerText, out Runtime.ImageEditor.DisplayAlpha);
                        break;
                    case "UseComponetSelector":
                        bool.TryParse(node.InnerText, out Runtime.ImageEditor.UseComponetSelector);
                        break;
                    case "EnableImageZoom":
                        bool.TryParse(node.InnerText, out Runtime.ImageEditor.EnableImageZoom);
                        break;
                    case "EnablePixelGrid":
                        bool.TryParse(node.InnerText, out Runtime.ImageEditor.EnablePixelGrid);
                        break;
                }
            }
        }

        private static bool TryParsePoint(XmlNode node, ref Point point)
        {
            try
            {
                string[] values = node.InnerText.Split(',');


                int x = -1;
                int y = -1;

                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Replace('=', ' ');
                    values[i] = values[i].Replace('}', ' ');
                    values[i] = values[i].Replace('{', ' ');

                    if (values[i].Contains("X"))
                    {
                        values[i] = values[i].Replace('X', ' ');
                        int.TryParse(values[i], out x);
                    }
                    if (values[i].Contains("Y"))
                    {
                        values[i] = values[i].Replace('Y', ' ');
                        int.TryParse(values[i], out y);
                    }
                }


                if (x == -1 || y == -1)
                    return false;

                point.X = x;
                point.Y = y;

                return true;
            }
            catch (Exception)
            {
                // Invalid hex format.
                return false;
            }
        }

        private static bool TryParseHexColor(XmlNode node, ref Color color)
        {
            try
            {
                color = ColorTranslator.FromHtml(node.InnerText);
                return true;
            }
            catch (Exception)
            {
                // Invalid hex format.
                return false;
            }
        }

        public static void Save()
        {
            XmlDocument doc = CreateXmlFromSettings();
            doc.Save(Runtime.ExecutableDir + "\\config.xml");

            SavePaths();
        }

        private static void SavePaths()
        {
            XmlDocument doc = CreatePathXmlFromSettings();
            doc.Save(Runtime.ExecutableDir + "\\config_paths.xml");
        }

        private static XmlDocument CreatePathXmlFromSettings()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode mainNode = doc.CreateElement("TOOLCONFIG");
            doc.AppendChild(mainNode);

            AppendPathSettings(doc, mainNode);

            return doc;
        }

        private static XmlDocument CreateXmlFromSettings()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode mainNode = doc.CreateElement("TOOLCONFIG");
            doc.AppendChild(mainNode);

            AppendMainFormSettings(doc, mainNode);
            AppendImageEditorSettings(doc, mainNode);
            AppendObjectlistSettings(doc, mainNode);
            AppendRenderSettings(doc, mainNode);
            AppendOCompressionFilelistSettings(doc, mainNode);
            AppendEditorSettings(doc, mainNode);
            AppendBackgroundSettings(doc, mainNode);
            AppendGridSettings(doc, mainNode);

            return doc;
        }
        private static void AppendMainFormSettings(XmlDocument doc, XmlNode parentNode)
        {
            XmlNode mainSettingsNode = doc.CreateElement("MAINFORM");
            parentNode.AppendChild(mainSettingsNode);

            
            mainSettingsNode.AppendChild(createNode(doc, "DisplayViewport", Runtime.DisplayViewport.ToString()));
            mainSettingsNode.AppendChild(createNode(doc, "UseOpenGL", Runtime.UseOpenGL.ToString()));
            mainSettingsNode.AppendChild(createNode(doc, "UseDebugDomainExceptionHandler", Runtime.UseDebugDomainExceptionHandler.ToString()));
            mainSettingsNode.AppendChild(createNode(doc, "OpenStartupWindow", Runtime.OpenStartupWindow.ToString()));
            mainSettingsNode.AppendChild(createNode(doc, "EnableVersionCheck", Runtime.EnableVersionCheck.ToString()));
            mainSettingsNode.AppendChild(createNode(doc, "FormTheme", Switch_Toolbox.Library.Forms.FormThemes.ActivePreset.ToString()));
            mainSettingsNode.AppendChild(createNode(doc, "MaximizeMdiWindow", Runtime.MaximizeMdiWindow.ToString()));
        }
        private static void AppendImageEditorSettings(XmlDocument doc, XmlNode parentNode)
        {
            XmlNode PathsNode = doc.CreateElement("ImageEditor");
            parentNode.AppendChild(PathsNode);
            PathsNode.AppendChild(createNode(doc, "DisplayVertical", Runtime.ImageEditor.DisplayVertical.ToString()));
            PathsNode.AppendChild(createNode(doc, "ShowPropertiesPanel", Runtime.ImageEditor.ShowPropertiesPanel.ToString()));
            PathsNode.AppendChild(createNode(doc, "DisplayAlpha", Runtime.ImageEditor.DisplayAlpha.ToString()));
            PathsNode.AppendChild(createNode(doc, "UseComponetSelector", Runtime.ImageEditor.UseComponetSelector.ToString()));
            PathsNode.AppendChild(createNode(doc, "EnablePixelGrid", Runtime.ImageEditor.EnablePixelGrid.ToString()));
            PathsNode.AppendChild(createNode(doc, "EnableImageZoom", Runtime.ImageEditor.EnableImageZoom.ToString()));
        }
        private static void AppendPathSettings(XmlDocument doc, XmlNode parentNode)
        {
            XmlNode PathsNode = doc.CreateElement("PATHS");
            parentNode.AppendChild(PathsNode);
            PathsNode.AppendChild(createNode(doc, "SmoGamePath", Runtime.SmoGamePath.ToString()));
            PathsNode.AppendChild(createNode(doc, "Mk8GamePath", Runtime.Mk8GamePath.ToString()));
            PathsNode.AppendChild(createNode(doc, "Mk8dGamePath", Runtime.Mk8dGamePath.ToString()));
            PathsNode.AppendChild(createNode(doc, "TpGamePath", Runtime.TpGamePath.ToString()));
            PathsNode.AppendChild(createNode(doc, "BotwGamePath", Runtime.BotwGamePath.ToString()));
        }
        private static void AppendEditorSettings(XmlDocument doc, XmlNode parentNode)
        {
            XmlNode SettingsNode = doc.CreateElement("EDITORSETTINGS");
            parentNode.AppendChild(SettingsNode);
            SettingsNode.AppendChild(createNode(doc, "PreferredTexFormat", Runtime.PreferredTexFormat.ToString()));
        }
        private static void AppendOCompressionFilelistSettings(XmlDocument doc, XmlNode parentNode)
        {
            XmlNode compSettingsNode = doc.CreateElement("COMPRESSIONSETTINGS");
            parentNode.AppendChild(compSettingsNode);
            compSettingsNode.AppendChild(createNode(doc, "Yaz0CompressionLevel", Runtime.Yaz0CompressionLevel.ToString()));
        }
        private static void AppendObjectlistSettings(XmlDocument doc, XmlNode parentNode)
        {
            XmlNode objlistSettingsNode = doc.CreateElement("OBJLISTSETTINGS");
            parentNode.AppendChild(objlistSettingsNode);
            objlistSettingsNode.AppendChild(createNode(doc, "thumbnailSize", Runtime.thumbnailSize.ToString()));
            objlistSettingsNode.AppendChild(createNode(doc, "ListPanelWidth", Runtime.ObjectEditor.ListPanelWidth.ToString()));
            objlistSettingsNode.AppendChild(createNode(doc, "AddFilesToActiveObjectEditor", Runtime.AddFilesToActiveObjectEditor.ToString()));
        }
        private static void AppendRenderSettings(XmlDocument doc, XmlNode parentNode)
        {
            XmlNode renderSettingsNode = doc.CreateElement("RENDERSETTINGS");
            parentNode.AppendChild(renderSettingsNode);
            renderSettingsNode.AppendChild(createNode(doc, "viewportShading", Runtime.viewportShading.ToString()));
            renderSettingsNode.AppendChild(createNode(doc, "stereoscopy", Runtime.stereoscopy.ToString()));
            renderSettingsNode.AppendChild(createNode(doc, "CameraFar", Runtime.CameraFar.ToString()));
            renderSettingsNode.AppendChild(createNode(doc, "CameraNear", Runtime.CameraNear.ToString()));
            renderSettingsNode.AppendChild(createNode(doc, "PreviewScale", Runtime.previewScale.ToString()));
            renderSettingsNode.AppendChild(createNode(doc, "EnablePBR", Runtime.EnablePBR.ToString()));
            renderSettingsNode.AppendChild(createNode(doc, "renderVertColor", Runtime.renderVertColor.ToString()));
            renderSettingsNode.AppendChild(createNode(doc, "renderReflection", Runtime.renderReflection.ToString()));
            renderSettingsNode.AppendChild(createNode(doc, "renderSpecular", Runtime.renderSpecular.ToString()));
            renderSettingsNode.AppendChild(createNode(doc, "renderBones", Runtime.renderBones.ToString()));
            renderSettingsNode.AppendChild(createNode(doc, "renderBoundingBoxes", Runtime.renderBoundingBoxes.ToString()));
            renderSettingsNode.AppendChild(createNode(doc, "useNormalMap", Runtime.useNormalMap.ToString()));
            renderSettingsNode.AppendChild(createNode(doc, "ViewportWidth", Runtime.ViewportEditor.Width.ToString()));
            renderSettingsNode.AppendChild(createNode(doc, "normalsLineLength", Runtime.normalsLineLength.ToString()));
            renderSettingsNode.AppendChild(createNode(doc, "bonePointSize", Runtime.bonePointSize.ToString()));
            renderSettingsNode.AppendChild(createNode(doc, "MaxCameraSpeed", Runtime.MaxCameraSpeed.ToString()));
        }

        private static void AppendGridSettings(XmlDocument doc, XmlNode parentNode)
        {
            parentNode.AppendChild(createNode(doc, "GridColor", ColorTranslator.ToHtml(Runtime.gridSettings.color)));
            parentNode.AppendChild(createNode(doc, "CellAmount", Runtime.gridSettings.CellAmount.ToString()));
            parentNode.AppendChild(createNode(doc, "CellSize", Runtime.gridSettings.CellSize.ToString()));
        }

        private static void AppendBackgroundSettings(XmlDocument doc, XmlNode parentNode)
        {
            parentNode.AppendChild(createNode(doc, "renderBackGround", Runtime.renderBackGround.ToString()));
            parentNode.AppendChild(createNode(doc, "backgroundGradientTop", ColorTranslator.ToHtml(Runtime.backgroundGradientTop)));
            parentNode.AppendChild(createNode(doc, "backgroundGradientBottom", ColorTranslator.ToHtml(Runtime.backgroundGradientBottom)));
        }

        public static XmlNode createNode(XmlDocument doc, string el, string v)
        {
            XmlNode floorstyle = doc.CreateElement(el);
            floorstyle.InnerText = v;
            return floorstyle;
        }
    }
}
