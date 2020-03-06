/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：sinvie                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2020-03-06 14:57:12                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Sinvie.Cms.Models                                  
*│　类    名：T_Article                                     
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sinvie.Cms.Models
{
	/// <summary>
	/// sinvie
	/// 2020-03-06 14:57:12
	/// 
	/// </summary>
	public partial class T_Article
	{
		/// <summary>
		/// 主键ID，自增长
		/// </summary>
		[Key]
		public Int32 Id {get;set;}

		/// <summary>
		/// 文章标题
		/// </summary>
		[Required]
		[MaxLength(255)]
		public String Title {get;set;}

		/// <summary>
		/// 文章副标题
		/// </summary>
		[Required]
		[MaxLength(255)]
		public String SubTitle {get;set;}

		/// <summary>
		/// 关键字
		/// </summary>
		[Required]
		[MaxLength(255)]
		public String KeyWords {get;set;}

		/// <summary>
		/// 描述
		/// </summary>
		[Required]
		[MaxLength(2000)]
		public String Description {get;set;}

		/// <summary>
		/// 分类ID
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 ClassID {get;set;}

		/// <summary>
		/// 封面大图
		/// </summary>
		[Required]
		[MaxLength(255)]
		public String BigImg {get;set;}

		/// <summary>
		/// 封面小图
		/// </summary>
		[Required]
		[MaxLength(255)]
		public String SmallImg {get;set;}

		/// <summary>
		/// 文章编辑
		/// </summary>
		[Required]
		[MaxLength(255)]
		public String NAuthor {get;set;}

		/// <summary>
		/// 文章来源
		/// </summary>
		[Required]
		[MaxLength(255)]
		public String NSource {get;set;}

		/// <summary>
		/// 文章外链以http开头
		/// </summary>
		[Required]
		[MaxLength(255)]
		public String NURL {get;set;}

		/// <summary>
		/// 添加时间
		/// </summary>
		[Required]
		[MaxLength(23)]
		public DateTime AddTime {get;set;}

		/// <summary>
		/// 编辑时间
		/// </summary>
		[Required]
		[MaxLength(23)]
		public DateTime EditTime {get;set;}

		/// <summary>
		/// 文章图文内容
		/// </summary>
		[Required]
		public String Contents {get;set;}

		/// <summary>
		/// 是否推荐
		/// </summary>
		[Required]
		[MaxLength(1)]
		public Boolean IsRem {get;set;}

		/// <summary>
		/// 是否置顶
		/// </summary>
		[Required]
		[MaxLength(1)]
		public Boolean IsTop {get;set;}

		/// <summary>
		/// 点击率
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 Hits {get;set;}

		/// <summary>
		/// 组织框架ID
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 OrgID {get;set;}

		/// <summary>
		/// 排序号
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 OrderBy {get;set;}

		/// <summary>
		/// 逻辑删除标志，true:未删除，false:已删除
		/// </summary>
		[Required]
		[MaxLength(1)]
		public Boolean Flag {get;set;}

		/// <summary>
		/// 备用字段:int
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 Remark1 {get;set;}

		/// <summary>
		/// 备用字段:string(255)
		/// </summary>
		[Required]
		[MaxLength(255)]
		public String Remark2 {get;set;}

		/// <summary>
		/// 备用字段:string(max)
		/// </summary>
		[Required]
		public String Remark3 {get;set;}

		/// <summary>
		/// 备用字段:string(255)
		/// </summary>
		[Required]
		[MaxLength(255)]
		public String Remark4 {get;set;}

		/// <summary>
		/// 备用字段:string(255)
		/// </summary>
		[Required]
		[MaxLength(255)]
		public String Remark5 {get;set;}


	}
}
