using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;

namespace FontAdjust
{
    /// <summary>
    /// Font Adjust System core
    /// </summary>
    public class FontAdjustCore
    {
        /// <summary>
        /// Bind Font and FontMetricsData
        /// </summary>
        private Dictionary<Font, FontMetricsData> metricDictionary;

        /// <summary>
        /// position up or down
        /// </summary>
        private bool positionUp = false;

        /// <summary>
        /// Constructer
        /// </summary>
        public FontAdjustCore()
        {
            this.metricDictionary = new Dictionary<Font, FontMetricsData>();
        }

        /// <summary>
        /// set up or down.
        /// </summary>
        /// <param name="flag">true if up</param>
        public void SetPositionUp(bool flag)
        {
            this.positionUp = flag;
        }

        /// <summary>
        /// Execute adjust
        /// </summary>
        /// <param name="text">UI TextComponent</param>
        /// <returns>true if changed</returns>
        public bool Execute(Text text)
        {
            Font font = text.font;
            if (font == null) { return false; }
            FontMetricsData metrics = this.GetMetricData(font);
            if (metrics == null) { return false; }

            float param = metrics.GetCalculatedLeading(text.fontSize) * GetAlignmentParameter(text.alignment);
            param *= text.rectTransform.localScale.y;

            float oldPositionY = text.rectTransform.position.y;
            if (positionUp)
            {
                text.rectTransform.localPosition = text.rectTransform.localPosition +
                    Vector3.up * param;
            }
            else
            {
                text.rectTransform.localPosition = text.rectTransform.localPosition +
                    Vector3.down * param;
            }
            // update children of textComponent
            foreach (RectTransform child in text.rectTransform)
            {
                child.localPosition = child.localPosition + Vector3.up * (oldPositionY - text.rectTransform.position.y) / text.rectTransform.lossyScale.y;
            }
            return true;
        }

        private FontMetricsData GetMetricData(Font font)
        {
            FontMetricsData data;
            if (this.metricDictionary.TryGetValue(font,out data))
            {
                return data;
            }
            data = FontMetricsData.CreateFontMetricsData(AssetDatabase.GetAssetPath(font));
            this.metricDictionary.Add(font, data);
            return data;
        }

        /// <summary>
        /// Get Align parameter
        /// </summary>
        /// <param name="anchor">anchor </param>
        /// <returns> </returns>
        private static float GetAlignmentParameter(TextAnchor anchor)
        {
            switch (anchor)
            {
                case TextAnchor.LowerCenter:
                case TextAnchor.LowerLeft:
                case TextAnchor.LowerRight:
                    return 1.0f;
                case TextAnchor.MiddleCenter:
                case TextAnchor.MiddleLeft:
                case TextAnchor.MiddleRight:
                    return 0.5f;
            }
            return 0.0f;
        }


    }

}