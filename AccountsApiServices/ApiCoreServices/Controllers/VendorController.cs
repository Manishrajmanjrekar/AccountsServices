﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCoreServices.Models;
using ApiCoreServices.SqlLayerInterfaces;
using ApiCoreServices.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCoreServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        IVendorRepository _VendorRepository;

        public VendorController(IVendorRepository VendorRepository)
        {
            _VendorRepository = VendorRepository;
        }

        [HttpPost]
        [Route("SaveVendor")]
        public CommonResponseViewModel SaveVendor(VendorViewModel vendorVM)
        {
            if (vendorVM.id > 0)
                return _VendorRepository.UpdateVendor(vendorVM);
            else
                return _VendorRepository.AddVendor(vendorVM);
        }

        [HttpPost]
        [Route("GetVendorById")]
        public VendorViewModel GetVendorById(int id)
        {
            VendorViewModel Vendorvm = null;
            Vendorvm = _VendorRepository.GetVendorById(id);
            return Vendorvm;

        }

        [Route("VendorNames")]
        [HttpPost]
        public List<VendorViewModel> VendorNames(AutoCompleteModel data)
        {
            return _VendorRepository.VendorNames(data);
        }



        [HttpPost]
        [Route("GetAllVendors")]
        public List<VendorViewModel> GetAllVendors()
        {
            return _VendorRepository.GetAllVendors();
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

            if ((_VendorRepository.CheckIsDuplicateNickName(data)))
            {
                isDuplicateNickName = true;
            }

            return isDuplicateNickName;
        }

        [HttpPost]
        [Route("DeleteVendorById")]
        public bool DeleteVendorById(int id)
        {
            var results = _VendorRepository.DeleteVendorById(id);
            return results;
        }


    }
}
