using CsvHelper;
using Gather.ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.Base;
using WebBanHang.Common.Interfaces.BL;
using WebBanHang.Common.Interfaces.DL;
using WebBanHang.Common.Services;

namespace WebBanHang.BL.BL
{
    public class ReceiverBL : BaseBL<Receiver>, IReceiverBL
    {
        IReceiverDL _receiverDL;

        public ReceiverBL(IBaseDL<Receiver> baseDL, IReceiverDL receiverDL) : base(baseDL)
        {
            _receiverDL = receiverDL;
        }

        /// <summary>
        /// Nhap khau
        /// </summary>
        /// <param name="formfile"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ServiceResult importReceiver(IFormFile file)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                List<Receiver> receivers = _receiverDL.GetAllEntities().ToList();
                var fileextension = Path.GetExtension(file.FileName);
                var filename = Guid.NewGuid().ToString() + fileextension;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Temp", filename);
                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    file.CopyTo(fs);
                }
                if (fileextension == ".csv")
                {
                    using (var reader = new StreamReader(filepath))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<ReceiverEmail>().ToList();
                        List<ReceiverEmail> listReceiver = new List<ReceiverEmail>();
                        foreach (var record in records)
                        {
                            if (!receivers.Any(x => x.email == record.Email))
                            {
                                listReceiver.Add(record);
                            }
                        }
                        if (listReceiver.Count > 0)
                        {
                            _receiverDL.importReceiver(listReceiver);
                        }
                    }
                    if (File.Exists(filepath))
                    {
                        // If file found, delete it    
                        File.Delete(filepath);
                    }
                }

                else
                {
                    serviceResult.setError("Không đúng định dạng file CSV.");
                }
            }
            catch (Exception e)
            {
                serviceResult.setError(e.ToString());
            }
            return serviceResult;
        }
    }
}
