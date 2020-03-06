/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：属性节点                                                    
*│　作    者：sinvie                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/1/5 12:16:25                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Sinvie.Cms.Core.Models                                   
*│　类    名： TreeItem                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Sinvie.Cms.Core.Models
{
    public class TreeItem<T>
    {
        public T Item { get; set; }
        public IEnumerable<TreeItem<T>> Children { get; set; }
    }
}
