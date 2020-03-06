/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：sinvie                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2020-03-06 14:57:12                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Sinvie.Cms.Models                                  
*│　类    名：T_AdminUser                                     
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
	public partial class T_AdminUser
	{
		/// <summary>
		/// 主键ID，自增长
		/// </summary>
		[Key]
		public Int32 Id {get;set;}

		/// <summary>
		/// 登录名称
		/// </summary>
		[Required]
		[MaxLength(255)]
		public String LoginName {get;set;}

		/// <summary>
		/// 加密密钥
		/// </summary>
		[Required]
		[MaxLength(50)]
		public String PassKey {get;set;}

		/// <summary>
		/// 登录密码
		/// </summary>
		[Required]
		[MaxLength(255)]
		public String LoginPass {get;set;}

		/// <summary>
		/// 姓名
		/// </summary>
		[Required]
		[MaxLength(50)]
		public String RealName {get;set;}

		/// <summary>
		/// 权限代码
		/// </summary>
		[Required]
		public String RuleCode {get;set;}

		/// <summary>
		/// 是否超级管理员
		/// </summary>
		[Required]
		[MaxLength(1)]
		public Boolean IsAdmin {get;set;}

		/// <summary>
		/// 组织结构ID
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 OrgID {get;set;}

		/// <summary>
		/// 逻辑删除标志，true:未删除，false:已删除
		/// </summary>
		[Required]
		[MaxLength(1)]
		public Boolean Flag {get;set;}

		/// <summary>
		/// 备用字段:int类型
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 Remark1 {get;set;}

		/// <summary>
		/// 备用字段:string(255)类型
		/// </summary>
		[Required]
		[MaxLength(255)]
		public String Remark2 {get;set;}

		/// <summary>
		/// 备用字段:bool类型
		/// </summary>
		[Required]
		[MaxLength(1)]
		public Boolean Remark3 {get;set;}


	}
}
