using System;
using System.Collections.Generic;
using System.Text;

namespace AcceleroRecorder.Object
{
    public class Frame
    {
        // Declare attribut
        private string time;
        private float xData;
        private float yData;
        private float zData;

        /// <summary>
        /// Constructor of the Frame object
        /// </summary>
        /// <param name="time"> Time of the frame </param>
        /// <param name="xData"> x data of the accelerometer a this time </param>
        /// <param name="yData"> y data of the accelerometer a this time</param>
        /// <param name="zData"> z data of the accelerometer a this time</param>
        public Frame(string time, float xData, float yData, float zData)
        {
            this.time = time;
            this.xData = xData;
            this.yData = yData;
            this.zData = zData;
        }

        // Encapse
        public string Time { get => time;/* set => time = value;*/ }
        public float XData { get => xData; /*set => xData = value;*/ }
        public float YData { get => yData; /*set => yData = value;*/ }
        public float ZData { get => zData; /*set => zData = value;*/ }
    }
}
