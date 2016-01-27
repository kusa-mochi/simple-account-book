using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace MainApplication
{
    public class CommonConst
    {
        public static readonly string ApplicationName = @"パパッと家計簿";
        public static readonly string DBFileName = @"kakeibodata.db";
        public static readonly Color MainMenuColor = Color.DimGray;
        public static readonly Color MainMenuHoverColor = Color.Gray;
        public static readonly Color TableHeaderColor = Color.LightBlue;
        public static readonly Color TargetAmountLineColor = Color.Red;
        public static readonly Color MonthControlButtonColor = Color.Yellow;
        public static readonly Color PrevNextMonthButtonColor = Color.Gold;
        public static readonly Color ItemButtonColor = Color.LightSkyBlue;
        public static readonly int MaxNumberOfKindOfAmount = 20;
        public static readonly int MaxNumberOfTransitionMonth = 12;

        //// Semi Transparent
        //public static readonly Color[] ColorPalette = new Color[] {
        //    Color.FromArgb(255,105,105),
        //    Color.FromArgb(105,255,105),
        //    Color.FromArgb(105,105,255),
        //    Color.FromArgb(255,255,105),
        //    Color.FromArgb(105,255,255),
        //    Color.FromArgb(255,105,255),
        //    Color.FromArgb(205,176,117),
        //    Color.FromArgb(255,175,175),
        //    Color.FromArgb(175,255,175),
        //    Color.FromArgb(175,175,255),
        //    Color.FromArgb(255,255,175),
        //    Color.FromArgb(175,255,255),
        //    Color.FromArgb(255,175,255),
        //    Color.FromArgb(228,213,181),
        //    Color.FromArgb(164,176,134),
        //    Color.FromArgb(129,158,193),
        //    Color.FromArgb(255,105,105),
        //    Color.FromArgb(105,255,105),
        //    Color.FromArgb(105,105,255),
        //    Color.FromArgb(255,255,105),
        //    Color.FromArgb(105,255,255),
        //    Color.FromArgb(255,105,255),
        //    Color.FromArgb(205,176,117),
        //    Color.FromArgb(255,175,175),
        //    Color.FromArgb(175,255,175),
        //    Color.FromArgb(175,175,255),
        //    Color.FromArgb(255,255,175),
        //    Color.FromArgb(175,255,255),
        //    Color.FromArgb(255,175,255),
        //    Color.FromArgb(228,213,181),
        //    Color.FromArgb(164,176,134),
        //    Color.FromArgb(129,158,193)
        //};

        //// Light
        //public static readonly Color[] ColorPalette = new Color[] {
        //    Color.FromArgb(230,230,250),
        //    Color.FromArgb(255,240,245),
        //    Color.FromArgb(255,218,185),
        //    Color.FromArgb(255,250,205),
        //    Color.FromArgb(255,228,225),
        //    Color.FromArgb(240,255,240),
        //    Color.FromArgb(240,248,255),
        //    Color.FromArgb(245,245,245),
        //    Color.FromArgb(250,235,215),
        //    Color.FromArgb(224,255,255),
        //    Color.FromArgb(230,230,250),
        //    Color.FromArgb(255,240,245),
        //    Color.FromArgb(255,218,185),
        //    Color.FromArgb(255,250,205),
        //    Color.FromArgb(255,228,225),
        //    Color.FromArgb(240,255,240),
        //    Color.FromArgb(240,248,255),
        //    Color.FromArgb(245,245,245),
        //    Color.FromArgb(250,235,215),
        //    Color.FromArgb(224,255,255),
        //    Color.FromArgb(230,230,250),
        //    Color.FromArgb(255,240,245),
        //    Color.FromArgb(255,218,185),
        //    Color.FromArgb(255,250,205),
        //    Color.FromArgb(255,228,225),
        //    Color.FromArgb(240,255,240),
        //    Color.FromArgb(240,248,255),
        //    Color.FromArgb(245,245,245),
        //    Color.FromArgb(250,235,215),
        //    Color.FromArgb(224,255,255)
        //};

        // Gray Scale
        public static readonly Color[] ColorPaletteTotalAmount = new Color[] {
            Color.FromArgb(0,0,0),
            Color.FromArgb(220,220,220),
            Color.FromArgb(210,210,210),
            Color.FromArgb(200,200,200),
            Color.FromArgb(190,190,190),
            Color.FromArgb(180,180,180),
            Color.FromArgb(170,170,170),
            Color.FromArgb(160,160,160),
            Color.FromArgb(150,150,150),
            Color.FromArgb(140,140,140),
            Color.FromArgb(130,130,130),
            Color.FromArgb(120,120,120),
            Color.FromArgb(110,110,110),
            Color.FromArgb(100,100,100),
            Color.FromArgb(90,90,90),
            Color.FromArgb(80,80,80),
            Color.FromArgb(70,70,70),
            Color.FromArgb(60,60,60),
            Color.FromArgb(50,50,50),
            Color.FromArgb(40,40,40),
            Color.FromArgb(30,30,30),
            Color.FromArgb(20,20,20),
            Color.FromArgb(10,10,10)
        };

        public static readonly Color[] ColorPalette = new Color[] {
            Color.FromArgb(180,180,180)
        };
    }
}
