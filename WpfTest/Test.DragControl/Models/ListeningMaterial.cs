using JsonSubTypes;
using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.ObjectModel;
using Test.DragControl.Utils;

namespace Test.DragControl.Models
{
    public class ListeningMaterial : ViewModelBase, ICloneable
    {
        private int materialId;
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int MaterialId
        {
            get { return materialId; }
            set
            {
                materialId = value;
                RaisePropertyChanged(nameof(MaterialId));
            }
        }

        private string title=$"听力材料_{DateTime.Now:yyyyMMdd}";
        /// <summary>
        /// 听力材料名
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        private string vision;
        /// <summary>
        /// 版本
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Vision
        {
            get { return vision; }
            set
            {
                vision = value;
                RaisePropertyChanged(nameof(Vision));
            }
        }

        private string volume;
        /// <summary>
        /// 册次
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                RaisePropertyChanged(nameof(Volume));
            }
        }

        private string module;
        /// <summary>
        /// 模块
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Module
        {
            get { return module; }
            set
            {
                module = value;
                RaisePropertyChanged(nameof(Module));
            }
        }

        private DateTime createTime = DateTime.Now;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set
            {
                createTime = value;
                RaisePropertyChanged(nameof(CreateTime));
            }
        }

        private DateTime updateTime = DateTime.Now;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            get { return updateTime; }
            set
            {
                updateTime = value;
                RaisePropertyChanged(nameof(UpdateTime));
            }
        }

        private string audioDownloadUrl;
        /// <summary>
        /// 音频合成后的下载路径
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string AudioDownloadUrl
        {
            get { return audioDownloadUrl; }
            set
            {
                audioDownloadUrl = value;
                RaisePropertyChanged(nameof(AudioDownloadUrl));
            }
        }

        private string contentJson;
        [SugarColumn(IsNullable = true, ColumnDataType = "longtext")]
        public string ContentJson
        {
            get => contentJson;
            set
            {
                contentJson = value;
                var temp = JsonConvert.DeserializeObject(contentJson);
                ContentList = JsonConvert.DeserializeObject<ObservableCollection<ListeningContent>>(temp.ToString());
            }
        }

        private ObservableCollection<ListeningContent> contentList =
            new ObservableCollection<ListeningContent>();
        /// <summary>
        /// 听力材料内容
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public ObservableCollection<ListeningContent> ContentList
        {
            get { return contentList; }
            set
            {
                contentList = value;
                RaisePropertyChanged(nameof(ContentList));
            }
        }

        /// <summary>
        /// 浅拷贝
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(ListeningTextContent), "ContentType")]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(ListeningPauseContent), "PauseTime")]
    public class ListeningContent : ViewModelBase
    {
        private string title;
        /// <summary>
        /// 内容标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        private string tag;
        /// <summary>
        /// 内容提示语(或者叫水印)
        /// </summary>
        public string Tag
        {
            get { return tag; }
            set
            {
                tag = value;
                RaisePropertyChanged(nameof(Tag));
            }
        }
    }

    public class ListeningTextContent : ListeningContent
    {
        private int contentType;
        /// <summary>
        /// 内容类型
        /// </summary>
        /// <remarks>
        /// 0:独白，1:对话，2:指导语
        /// </remarks>
        public int ContentType
        {
            get { return contentType; }
            set
            {
                contentType = value;
                RaisePropertyChanged(nameof(ContentType));
            }
        }

        private string content;
        /// <summary>
        /// 文本内容
        /// </summary>
        public string Content
        {
            get { return content; }
            set
            {
                content = value;
                RaisePropertyChanged(nameof(Content));
            }
        }

        private AudioInfo audio;
        /// <summary>
        /// 音频信息
        /// </summary>
        public AudioInfo Audio
        {
            get { return audio; }
            set
            {
                audio = value;
                RaisePropertyChanged(nameof(Audio));
            }
        }
    }

    public class ListeningPauseContent : ListeningContent
    {
        private int psuseTime;
        /// <summary>
        /// 停顿时长(秒)
        /// </summary>
        public int PauseTime
        {
            get { return psuseTime; }
            set
            {
                psuseTime = value;
                RaisePropertyChanged(nameof(PauseTime));
            }
        }
    }

    public class AudioInfo : ViewModelBase
    {
        private bool isReadOneTime = true;
        /// <summary>
        /// 是否读一遍
        /// </summary>
        /// <remarks>
        /// 默认读一遍，否则读两遍
        /// </remarks>
        public bool IsReadOneTime
        {
            get { return isReadOneTime; }
            set
            {
                isReadOneTime = value;
                RaisePropertyChanged(nameof(IsReadOneTime));
            }
        }

        private bool isMaleVoice = true;
        /// <summary>
        /// 是否男声
        /// </summary>
        /// <remarks>
        /// 默认男声，否则女生
        /// </remarks>
        public bool IsMaleVoice
        {
            get { return isMaleVoice; }
            set
            {
                isMaleVoice = value;
                RaisePropertyChanged(nameof(IsMaleVoice));
            }
        }

        private string audioUrl;
        /// <summary>
        /// 音频Url
        /// </summary>
        public string AudioUrl
        {
            get { return audioUrl; }
            set
            {
                audioUrl = value;
                RaisePropertyChanged(nameof(AudioUrl));
            }
        }

        private string audioFileName;
        /// <summary>
        /// 音频文件名
        /// </summary>
        public string AudioFileName
        {
            get { return audioFileName; }
            set
            {
                audioFileName = value;
                RaisePropertyChanged(nameof(AudioFileName));
            }
        }

        private double mAudioProgressValue;
        /// <summary>
        /// 当前播放进度
        /// </summary>
        [JsonIgnore]
        public double AudioProgressValue
        {
            get
            {
                return mAudioProgressValue;
            }
            set
            {
                if (mAudioProgressValue != value)
                {
                    mAudioProgressValue = value;
                    RaisePropertyChanged(nameof(AudioProgressValue));
                }
            }
        }

        private TimeSpan audioCurrentTime;
        /// <summary>
        /// 音频当前播放时长
        /// </summary>
        [JsonIgnore]
        public TimeSpan AudioCurrentTime
        {
            get { return audioCurrentTime; }
            set
            {
                audioCurrentTime = value;
                RaisePropertyChanged(nameof(AudioCurrentTime));
            }
        }

        private TimeSpan audioTotalTime;
        /// <summary>
        /// 音频总时长
        /// </summary>
        [JsonIgnore]
        public TimeSpan AudioTotalTime
        {
            get { return audioTotalTime; }
            set
            {
                audioTotalTime = value;
                RaisePropertyChanged(nameof(AudioTotalTime));
            }
        }
    }
}
