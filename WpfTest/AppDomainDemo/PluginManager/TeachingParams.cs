using System.Collections.Generic;

namespace PluginManager
{
    public class TeachingParams
    {
        public string ID { get; set; }

        public string Name { get; set; }
    }

    public class EBookTeachingParams : TeachingParams
    {
        public string Key { get; set; }

        public string Root { get; set; }

        public List<string> Rigths { get; set; }

        public string BookSeriesInfo { get; set; }

        /// <summary>
        /// 关联教材信息
        /// </summary>
        public List<string> RelatedBooksInfo { get; set; }

        public string UserId { get; set; }

        public string UserToken { get; set; }

        /// <summary>
        /// 是否单机版,默认false
        /// </summary>
        public bool IsSingle { get; set; }
        /// <summary>
        /// 单机版分片信息
        /// </summary>
        public string[] IncludePages { get; set; }

        public string PageName { get; set; }
    }

    public class FlashcardTeachingParams : TeachingParams
    {
        public string Words { get; set; }
        public string Catalogs { get; set; }
        public string CurrentCatalogId { get; set; }
    }

    public class BlackboardTeachingParams : TeachingParams
    {
        /// <summary>
        /// 打开的黑板所属父ID
        /// 如EBook_abc / Workarea_abc
        /// </summary>
        public string ParentId { get; set; }
    }
}
