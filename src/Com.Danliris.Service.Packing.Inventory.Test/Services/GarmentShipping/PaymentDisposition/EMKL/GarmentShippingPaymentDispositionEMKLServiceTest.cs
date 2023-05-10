﻿using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDisposition.PaymentDispositionEMKLs;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDisposition;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.PaymentDisposition
{
    public class GarmentShippingPaymentDispositionEMKLServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingPaymentDispositionRepository repository)
        {
            HttpResponseMessage message = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingPaymentDispositionRepository)))
                .Returns(repository);

            return spMock;
        }

        protected GarmentShippingPaymentDispositionEMKLService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShippingPaymentDispositionEMKLService(serviceProvider);
        }

        protected GarmentShippingPaymentDispositionEMKLViewModel ViewModel
        {
            get
            {
                return new GarmentShippingPaymentDispositionEMKLViewModel
                {
                    unitCharges = new List<GarmentShippingPaymentDispositionUnitChargeViewModel>()
                    {
                        new GarmentShippingPaymentDispositionUnitChargeViewModel()
                    },
                    invoiceDetails = new List<GarmentShippingPaymentDispositionEMKLInvoiceDetailViewModel>()
                    {
                        new GarmentShippingPaymentDispositionEMKLInvoiceDetailViewModel()
                    },
                    billDetails= new List<GarmentShippingPaymentDispositionBillDetailViewModel>()
                    {
                        new GarmentShippingPaymentDispositionBillDetailViewModel()
                    },
                    paymentDetails = new List<GarmentShippingPaymentDispositionPaymentDetailViewModel>()
                    {
                        new GarmentShippingPaymentDispositionPaymentDetailViewModel()
                    }
                };
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentShippingPaymentDispositionRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentShippingPaymentDispositionModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingPaymentDispositionModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

      

        [Fact]
        public async Task Create_Success_EMKL()
        {
            var repoMock = new Mock<IGarmentShippingPaymentDispositionRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentShippingPaymentDispositionModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingPaymentDispositionModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);
            var ViewModel = this.ViewModel;
            ViewModel.paymentType = "EMKL";
            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentShippingPaymentDispositionModel("", "", "", "", "", 1, "", "", "", 1, "", "", 1, "", "", 1, "", "", 1, "", "", "", "", "", DateTimeOffset.Now, "", 1, 1, 1, "", 1, 1, 1, DateTimeOffset.Now, "", "", true, "", "", DateTimeOffset.Now, "", "", "",new List<GarmentShippingPaymentDispositionInvoiceDetailModel>(),new List<GarmentShippingPaymentDispositionBillDetailModel>(), new List<GarmentShippingPaymentDispositionUnitChargeModel>(), new List<GarmentShippingPaymentDispositionPaymentDetailModel>());

            var repoMock = new Mock<IGarmentShippingPaymentDispositionRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingPaymentDispositionModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var bills = new HashSet<GarmentShippingPaymentDispositionBillDetailModel> { new GarmentShippingPaymentDispositionBillDetailModel("", 1) };
            var units = new HashSet<GarmentShippingPaymentDispositionUnitChargeModel> { new GarmentShippingPaymentDispositionUnitChargeModel(1, "", 1, 1) };
            var invoices = new HashSet<GarmentShippingPaymentDispositionInvoiceDetailModel> { new GarmentShippingPaymentDispositionInvoiceDetailModel("", 1, 1, 1, 1, 1, 1, 1, 1, "", "") };
            var payments = new HashSet<GarmentShippingPaymentDispositionPaymentDetailModel> { new GarmentShippingPaymentDispositionPaymentDetailModel(DateTimeOffset.Now,"", 1) };

            var model = new GarmentShippingPaymentDispositionModel("", "", "", "", "", 1, "", "", "", 1, "", "", 1, "", "", 1, "", "", 1, "", "", "", "", "", DateTimeOffset.Now, "", 1, 1, 1, "", 1, 1, 1, DateTimeOffset.Now, "", "", true, "", "", DateTimeOffset.Now, "", "", "", invoices, bills, units, payments);

            var repoMock = new Mock<IGarmentShippingPaymentDispositionRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_Success()
        {
            var repoMock = new Mock<IGarmentShippingPaymentDispositionRepository>();
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentShippingPaymentDispositionModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var repoMock = new Mock<IGarmentShippingPaymentDispositionRepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }
    }
}
