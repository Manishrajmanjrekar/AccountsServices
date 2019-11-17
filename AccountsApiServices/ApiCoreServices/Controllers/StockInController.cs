using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCoreServices.Models;
using ApiCoreServices.ServicesModels;
using ApiCoreServices.SqlLayerInterfaces;
using ApiCoreServices.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCoreServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockInController : ControllerBase
    {
        IStockInRepository _StockInRepository;


        public StockInController(IStockInRepository StockInRepository)
        {
            _StockInRepository = StockInRepository;
        }


        [HttpPost]
        [Route("StockById")]
        public StockInViewModel StockById(string id)
        {
           return _StockInRepository.StockById(id);
        }

        [Route("LoadNames")]
        [HttpPost]
        public List<SalesViewModel> LoadNames(AutoCompleteModel data)
        {
            return _StockInRepository.LoadNames(data);
        }


        [HttpPost]
        [Route("StockInCount")]
        public int StockInCount(StockInViewModel stock)
        {
            return _StockInRepository.StockInCount(stock);
        }

        [HttpPost]
        [Route("SaveStock")]
        public CommonResponseViewModel SaveStock(StockInViewModel stockInVM)
        {
            if (stockInVM.id > 0)
                return _StockInRepository.UpdateStock (stockInVM);
            else
                return _StockInRepository.AddStock(stockInVM);
        }



        [HttpPost]
        [Route("GetAllStock")]
        public List<StockInViewModel> GetAllStockList()
        {
            return _StockInRepository.GetAllStockList();
        }

        [Route("CheckIsDuplicateNickName")]
        [HttpPost]
        public bool CheckIsDuplicateNickName([FromBody]string data)
        {
            bool isDuplicateNickName = false;

            if (string.IsNullOrWhiteSpace(data))
            {
                throw new Exception("name cannot be empty exception");
            }
           
            if ((_StockInRepository.CheckIsDuplicateNickName(data)))
            {
                isDuplicateNickName = true;
            }

            return isDuplicateNickName;
        }

        [HttpPost]
        [Route("DeleteStockById")]
        public bool DeleteStockById(int id)
        {
           var results = _StockInRepository.DeleteStockById(id);
           return results;
        }


    }
}
