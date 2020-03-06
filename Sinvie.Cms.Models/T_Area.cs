/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：sinvie                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2020-03-06 14:57:12                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Sinvie.Cms.Models                                  
*│　类    名：T_Area                                     
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
	public partial class T_Area
	{
		/// <summary>
		/// 主键ID，自增长
		/// </summary>
		[Key]
		public Int32 Id {get;set;}

		/// <summary>
		/// 区域名称
		/// </summary>
		[Required]
		[MaxLength(50)]
		public String Title {get;set;}

		/// <summary>
		/// 区域编码
		/// </summary>
		[Required]
		[MaxLength(50)]
		public String AreaCode {get;set;}

		/// <summary>
		/// 所属省
		/// </summary>
		[Required]
		[MaxLength(50)]
		public String Province {get;set;}

		/// <summary>
		/// 地级城市
		/// </summary>
		[Required]
		[MaxLength(50)]
		public String CityName {get;set;}

		/// <summary>
		/// 县级市
		/// </summary>
		[Required]
		[MaxLength(50)]
		public String District {get;set;}

		/// <summary>
		/// 经度
		/// </summary>
		[Required]
		[MaxLength(18)]
		public Decimal CenterLng {get;set;}

		/// <summary>
		/// 纬度
		/// </summary>
		[Required]
		[MaxLength(18)]
		public Decimal CenterLat {get;set;}

		/// <summary>
		/// 城市服务区域
		/// </summary>
		[Required]
		public String AreaPoint {get;set;}

		/// <summary>
		/// 空间存储
		/// </summary>
		[Required]
		public String GeoArea {get;set;}

		/// <summary>
		/// 备用
		/// </summary>
		[Required]
		public String AreaTemp {get;set;}

		/// <summary>
		/// 是否默认区域
		/// </summary>
		[Required]
		[MaxLength(1)]
		public Boolean IsDefault {get;set;}

		/// <summary>
		/// 上线状态
		/// </summary>
		[Required]
		[MaxLength(1)]
		public Boolean IsOnline {get;set;}

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
		/// 备用int
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 Remark1 {get;set;}

		/// <summary>
		/// 备用string
		/// </summary>
		[Required]
		[MaxLength(250)]
		public String Remark2 {get;set;}

		/// <summary>
		/// 备用bool
		/// </summary>
		[Required]
		[MaxLength(1)]
		public Boolean Remark3 {get;set;}


	}
}
