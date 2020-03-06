/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：sinvie                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2020-03-06 14:57:12                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Sinvie.Cms.Services                                  
*│　类    名： T_PowerService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using Sinvie.Cms.IRepository;
using Sinvie.Cms.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sinvie.Cms.Services
{
    public class T_PowerService: IT_PowerService
    {
        private readonly IT_PowerRepository _repository;

        public T_PowerService(IT_PowerRepository repository)
        {
            _repository = repository;
        }
    }
}