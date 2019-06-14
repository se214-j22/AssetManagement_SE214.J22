using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.TaiSans;
using GWebsite.AbpZeroTemplate.Application.Share.TaiSans.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.LoTaiSans.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.CTTaiSans.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System.Linq;
using System.Linq.Dynamic.Core;
using System;
using GWebsite.AbpZeroTemplate.Application.Share.NhomTaiSans.Dto;

namespace GWebsite.AbpZeroTemplate.Web.Core.TaiSans
{
    [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient)]
    public class TaiSanAppService : GWebsiteAppServiceBase, ITaiSanAppService
    {
        private readonly IRepository<ThongTinTaiSan> taisanrepository;
        private readonly IRepository<LoTaiSan> lotaisanrepository;
        private readonly IRepository<CTTaiSan> cttaisanrepository;
        private readonly IRepository<NhomTaiSan> nhomTaiSanrepository;
        public TaiSanAppService(IRepository<ThongTinTaiSan> taisanrepository, IRepository<LoTaiSan> lotaisanrepository, 
            IRepository<CTTaiSan> cttaisanrepository
            , IRepository<NhomTaiSan> nhomTaiSanrepository)
        {
            this.taisanrepository = taisanrepository;
            this.lotaisanrepository = lotaisanrepository;
            this.cttaisanrepository = cttaisanrepository;
            this.nhomTaiSanrepository = nhomTaiSanrepository;
        }
        public void CreateOrEditTaiSan(TaiSanInput taiSanInput)
        {
            if (taiSanInput.Id == 0)
            {
                Create(taiSanInput);
            }
            else
            {
                Update(taiSanInput);
            }
        }

        public void DeleteTaiSan(int id)
        {
            var taisanEntity = taisanrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (taisanEntity != null)
            {
                taisanEntity.IsDelete = true;
                taisanrepository.Update(taisanEntity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        public TaiSanInput GetTaiSanForEdit(int id)
        {
            var taisanEntity = taisanrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (taisanEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<TaiSanInput>(taisanEntity);
        }

        public TaiSanForViewDto GetTaiSanForView(int id)
        {
            var taisanEntity = taisanrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (taisanEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<TaiSanForViewDto>(taisanEntity);
        }

        //begin filterXuat
        public PagedResultDto<TaiSanDto> GetTaiSansXuat(TaiSanFilter input)
        {
            var query = taisanrepository.GetAll().Where(x => !x.IsDelete).Where(x => x.TinhTrang == "Cấp phát" || x.TinhTrang == "Điều chuyển" || x.TinhTrang == "Sửa chữa");

            // filter by value
            if (input.TenTs != null)
            {
                query = query.Where(x => x.TenTs.ToLower().Contains(input.TenTs));
            }
            if (input.MaTS != null)
            {
                query = query.Where(x => x.MaTS.ToLower().Contains(input.MaTS));
            }
            if (input.LoaiTS != null)
            {
                query = query.Where(x => x.LoaiTS.ToLower().Contains(input.LoaiTS));
            }
            if (input.TenNhomTS != null)
            {
                query = query.Where(x => x.TenNhomTS.ToLower().Contains(input.TenNhomTS));
            }
            if (input.SoSeri != null)
            {
                query = query.Where(x => x.Soseri.ToLower().Contains(input.SoSeri));
            }
            if (input.TenDV != null)
            {
                query = query.Where(x => x.TenDV.ToLower().Contains(input.TenDV));
            }

            var totalCount = query.Count();

            // sorting
            if (!string.IsNullOrWhiteSpace(input.Sorting))
            {
                query = query.OrderBy(input.Sorting);
            }

            // paging
            var items = query.PageBy(input).ToList();

            // result
            return new PagedResultDto<TaiSanDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<TaiSanDto>(item)).ToList());
        }
        //end filterXuat

        //begin filterTon
        public PagedResultDto<TaiSanDto> GetTaiSansTon(TaiSanFilter input)
        {
            var query = taisanrepository.GetAll().Where(x => !x.IsDelete).Where(x => x.TinhTrang == "Tồn kho" || x.TinhTrang == "Thu hồi");

            // filter by value
            if (input.TenTs != null)
            {
                query = query.Where(x => x.TenTs.ToLower().Contains(input.TenTs));
            }
            if (input.MaTS != null)
            {
                query = query.Where(x => x.MaTS.ToLower().Contains(input.MaTS));
            }
            if (input.LoaiTS != null)
            {
                query = query.Where(x => x.LoaiTS.ToLower().Contains(input.LoaiTS));
            }
            if (input.TenNhomTS != null)
            {
                query = query.Where(x => x.TenNhomTS.ToLower().Contains(input.TenNhomTS));
            }
            if (input.SoSeri != null)
            {
                query = query.Where(x => x.Soseri.ToLower().Contains(input.SoSeri));
            }
            if (input.TenDV != null)
            {
                query = query.Where(x => x.TenDV.ToLower().Contains(input.TenDV));
            }

            var totalCount = query.Count();

            // sorting
            if (!string.IsNullOrWhiteSpace(input.Sorting))
            {
                query = query.OrderBy(input.Sorting);
            }

            // paging
            var items = query.PageBy(input).ToList();

            // result
            return new PagedResultDto<TaiSanDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<TaiSanDto>(item)).ToList());
        }
        //end filterTon

        public PagedResultDto<TaiSanDto> GetTaiSans(TaiSanFilter input)
        {
            var query = taisanrepository.GetAll().Where(x => !x.IsDelete);

            // filter by value
            if (input.TenTs != null)
            {
                query = query.Where(x => x.TenTs.ToLower().Contains(input.TenTs));
            }
            if (input.MaTS != null)
            {
                query = query.Where(x => x.MaTS.ToLower().Contains(input.MaTS));
            }
            if (input.LoaiTS != null)
            {
                query = query.Where(x => x.LoaiTS.ToLower().Contains(input.LoaiTS));
            }
            if (input.TenNhomTS != null)
            {
                query = query.Where(x => x.TenNhomTS.ToLower().Contains(input.TenNhomTS));
            }
            if (input.SoSeri != null)
            {
                query = query.Where(x => x.Soseri.ToLower().Contains(input.SoSeri));
            }
            if (input.TenDV != null)
            {
                query = query.Where(x => x.TenDV.ToLower().Contains(input.TenDV));
            }

            var totalCount = query.Count();

            // sorting
            if (!string.IsNullOrWhiteSpace(input.Sorting))
            {
                query = query.OrderBy(input.Sorting);
            }

            // paging
            var items = query.PageBy(input).ToList();

            // result
            return new PagedResultDto<TaiSanDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<TaiSanDto>(item)).ToList());
        }

        public string[] GetArrTenNhomTaiSan(string loaiTS)
        {
            //var query = nhomTaiSanrepository.GetAll().Where(x => !x.IsDelete).Select(x => x.tenNhomTaiSan).ToArray();
            var query = nhomTaiSanrepository.GetAll().Where(x => !x.IsDelete).Where(x => x.loaiTaiSan == loaiTS).Select(x => x.tenNhomTaiSan).ToArray();
            string[] str = query.Select(x => x.ToString()).ToArray();
            return str;
        }
        public TaiSanInput getSoLuongTonTaiSan (int id)
        {
            var taisanEntity = taisanrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
            if (taisanEntity == null)
            {
                return null;
            }
            return ObjectMapper.Map<TaiSanInput>(taisanEntity);
        }

        public NhomTaiSanInput GetKhauHao(string tenNhomTS)
        {
            var query = nhomTaiSanrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.tenNhomTaiSan == tenNhomTS);
            NhomTaiSanInput khauHao = ObjectMapper.Map<NhomTaiSanInput>(query);
            return khauHao;
        }

        #region Private Method

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Create)]
        private void Create(TaiSanInput taiSanInput)
        {
            string[] dssoseri = taiSanInput.Soseri.Split(',');
            if(dssoseri.Count()>taiSanInput.SoLuong)
            {

            }
            else 
            {
                for (int i = 0; i < dssoseri.Count(); i++)
                {
                    var manhomtaisan = nhomTaiSanrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.tenNhomTaiSan == taiSanInput.TenNhomTS).Id;
                    taiSanInput.NgayNhap = DateTime.Now.Date;
                    taiSanInput.Soseri = dssoseri[i];
                    taiSanInput.MaNhomTS = manhomtaisan;
                    taiSanInput.TenDV = "Đang ở trong kho";
                    taiSanInput.TinhTrang = "Tồn kho";
                    taiSanInput.MaDV = 0;
                    var taisanEnity = ObjectMapper.Map<ThongTinTaiSan>(taiSanInput);
                    SetAuditInsert(taisanEnity);
                    taisanrepository.Insert(taisanEnity);
                    CurrentUnitOfWork.SaveChanges();

                    if (taisanEnity.LoaiTS == "Công cụ lao động")
                    {
                        taisanEnity.MaTS = "C";
                    }
                    else
                    {
                        taisanEnity.MaTS = "T";
                    }

                    switch (taisanEnity.MaNhomTS.ToString().Length)
                    {
                        case 1:
                            taisanEnity.MaTS += "00" + manhomtaisan.ToString();
                            break;
                        case 2:
                            taisanEnity.MaTS += "0" + manhomtaisan.ToString();
                            break;
                        case 3:
                            taisanEnity.MaTS += manhomtaisan.ToString();
                            break;
                        default:
                            break;
                    }
                    switch (taisanEnity.Id.ToString().Length)
                    {
                        case 1:
                            taisanEnity.MaTS += "00000" + taisanEnity.Id;
                            break;
                        case 2:
                            taisanEnity.MaTS += "0000" + taisanEnity.Id;
                            break;
                        case 3:
                            taisanEnity.MaTS += "000" + taisanEnity.Id;
                            break;
                        case 4:
                            taisanEnity.MaTS += "00" + taisanEnity.Id;
                            break;
                        case 5:
                            taisanEnity.MaTS += "0" + taisanEnity.Id;
                            break;
                        case 6:
                            taisanEnity.MaTS += taisanEnity.Id;
                            break;
                        default:
                            break;
                    }
                }
                for (int i = dssoseri.Count(); i < taiSanInput.SoLuong; i++)
                {
                    var manhomtaisan = nhomTaiSanrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.tenNhomTaiSan == taiSanInput.TenNhomTS).Id;
                    taiSanInput.NgayNhap = DateTime.Now.Date;
                    taiSanInput.MaNhomTS = manhomtaisan;
                    taiSanInput.Soseri = "";
                    taiSanInput.TenDV = "Đang ở trong kho";
                    taiSanInput.TinhTrang = "Tồn kho";
                    taiSanInput.MaDV = 0;
                    var taisanEnity = ObjectMapper.Map<ThongTinTaiSan>(taiSanInput);
                    SetAuditInsert(taisanEnity);
                    taisanrepository.Insert(taisanEnity);
                    CurrentUnitOfWork.SaveChanges();

                    if (taisanEnity.LoaiTS == "Công cụ lao động")
                    {
                        taisanEnity.MaTS = "C";
                    }
                    else
                    {
                        taisanEnity.MaTS = "T";
                    }

                    switch (taisanEnity.MaNhomTS.ToString().Length)
                    {
                        case 1:
                            taisanEnity.MaTS += "00" + manhomtaisan.ToString();
                            break;
                        case 2:
                            taisanEnity.MaTS += "0" + manhomtaisan.ToString();
                            break;
                        case 3:
                            taisanEnity.MaTS += manhomtaisan.ToString();
                            break;
                        default:
                            break;
                    }
                    switch (taisanEnity.Id.ToString().Length)
                    {
                        case 1:
                            taisanEnity.MaTS += "00000" + taisanEnity.Id;
                            break;
                        case 2:
                            taisanEnity.MaTS += "0000" + taisanEnity.Id;
                            break;
                        case 3:
                            taisanEnity.MaTS += "000" + taisanEnity.Id;
                            break;
                        case 4:
                            taisanEnity.MaTS += "00" + taisanEnity.Id;
                            break;
                        case 5:
                            taisanEnity.MaTS += "0" + taisanEnity.Id;
                            break;
                        case 6:
                            taisanEnity.MaTS += taisanEnity.Id;
                            break;
                        default:
                            break;
                    }
                }
            }
           


            //for (int i = 0; i < taiSanInput.SoLuong; i++)
            //{

                //    var manhomtaisan = nhomTaiSanrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.tenNhomTaiSan == taiSanInput.TenNhomTS).Id;
                //    taiSanInput.NgayNhap = DateTime.Now.Date;
                //    taiSanInput.MaNhomTS = manhomtaisan;
                //    taiSanInput.TenDV = "Đang ở trong kho";
                //    taiSanInput.TinhTrang = "Tồn kho";
                //    taiSanInput.MaDV = 0;
                //    var taisanEnity = ObjectMapper.Map<ThongTinTaiSan>(taiSanInput);
                //    SetAuditInsert(taisanEnity);
                //    taisanrepository.Insert(taisanEnity);
                //    CurrentUnitOfWork.SaveChanges();

                //    if(taisanEnity.LoaiTS== "Công cụ lao động")
                //    {
                //        taisanEnity.MaTS = "C";
                //    }else
                //    {
                //        taisanEnity.MaTS = "T";
                //    }

                //    switch (taisanEnity.MaNhomTS.ToString().Length)
                //    {
                //        case 1:
                //            taisanEnity.MaTS +="00" + manhomtaisan.ToString();
                //            break;
                //        case 2:
                //            taisanEnity.MaTS +="0" + manhomtaisan.ToString();
                //            break;
                //        case 3:
                //            taisanEnity.MaTS +=manhomtaisan.ToString();
                //            break;
                //        default:
                //            break;
                //    }
                //    switch (taisanEnity.Id.ToString().Length)
                //    {
                //        case 1:
                //            taisanEnity.MaTS += "00000" + taisanEnity.Id;
                //            break;
                //        case 2:
                //            taisanEnity.MaTS += "0000" + taisanEnity.Id;
                //            break;
                //        case 3:
                //            taisanEnity.MaTS += "000" + taisanEnity.Id;
                //            break;
                //        case 4:
                //            taisanEnity.MaTS += "00" + taisanEnity.Id;
                //            break;
                //        case 5:
                //            taisanEnity.MaTS += "0" + taisanEnity.Id;
                //            break;
                //        case 6:
                //            taisanEnity.MaTS += taisanEnity.Id;
                //            break;
                //        default:
                //            break;
                //    }
                //}
        }

        [AbpAuthorize(GWebsitePermissions.Pages_Administration_MenuClient_Edit)]
        private void Update(TaiSanInput taiSanInput)
        {
            
            var taisanEnity = taisanrepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == taiSanInput.Id);
            if (taisanEnity == null)
            {
            }
            ObjectMapper.Map(taiSanInput, taisanEnity);
            SetAuditEdit(taisanEnity);
            taisanrepository.Update(taisanEnity);
            CurrentUnitOfWork.SaveChanges();
        }

        #endregion
    }
}
