using Sinvie.Cms.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sinvie.Cms.Core.Options
{
    /// <summary>
    /// sinvie
    /// 2020.03.05
    /// 代码生成选项
    /// </summary>
    public class CodeGenerateOption:DbOption
    {
       
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 代码生成时间
        /// </summary>
        public string GeneratorTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        /// <summary>
        /// 输出路径（即解决方案的路径）
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        /// 实体命名空间（默认为实体层的存储目录名称）
        /// </summary>
        public string ModelsNamespace { get; set; }
        /// <summary>
        /// 仓储接口命名空间（默认为仓储接口层的存储目录名称）
        /// </summary>
        public string IRepositoryNamespace { get; set; }
        /// <summary>
        /// 仓储命名空间（默认为仓储层的存储目录名称）
        /// </summary>
        public string RepositoryNamespace { get; set; }
        /// <summary>
        /// 服务接口命名空间（默认为服务接口层的存储目录名称）
        /// </summary>
        public string IServicesNamespace { get; set; }
        /// <summary>
        /// 服务命名空间（默认为服务层的存储目录名称）
        /// </summary>
        public string ServicesNamespace { get; set; }
    }
}
