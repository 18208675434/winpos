using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QDAMAPOS.Model
{

    public class MediaList
    {
        //视频默认图片
        public string defaultvideoimg { get; set; }
        public List<string> img { get; set; }
        public object[] video { get; set; }
        public List<Mediadetaildto> mediadetaildtos { get; set; }
    }

    public class Mediadetaildto
    {
        /// <summary>
        /// 视屏或图片链接
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 媒体类型 1图片  2视屏
        /// </summary>
        public int mediatype { get; set; }

        /// <summary>
        /// 展示顺序
        /// </summary>
        public int sortnum { get; set; }
    }

}
