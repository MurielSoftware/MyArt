using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Context;
using Shared.Dtos.Paintings;
using PagedList;
using Shared.Core.Dtos;
using Server.Model;
using Shared.Dtos.Resources;
using Shared.Core.Context.Expressions;
using Shared.Core.Dtos.Resources;

namespace Server.Daos
{
    public class PaintingDao : BaseDao
    {
        public PaintingDao(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public IPagedList<PaintingDto> FindPaged(PaintingFilterDto paintingFilterDto)
        {
            return _modelContext.Set<Painting>()
                .Where(ExpressionBuilder.BuildWhere<Painting>(paintingFilterDto))
                .OrderBy(x => x.Title)
                .Select(x => new PaintingDto() {
                    Id = x.Id,
                    Title = x.Title,
                    Technique = x.Technique,
                    Surface = x.Surface,
                    Width = x.Width,
                    Height = x.Height,
                    CoverPhotoResourceDto = x.Galleries.Where(y => (y is ProfileGallery)).Select(y => new PhotoResourceDto() { Id = y.CoverPhoto.Id, Path = y.CoverPhoto.Path, Name = y.CoverPhoto.Name }).FirstOrDefault()
                })
                .ToPagedList(paintingFilterDto.Page, paintingFilterDto.PageSize);
        }

        public IList<PaintingDto> FindAll(PaintingFilterDto paintingFilterDto)
        {
            return _modelContext.Set<Painting>()
                .Where(ExpressionBuilder.BuildWhere<Painting>(paintingFilterDto))
                .OrderBy(x => x.Title)
                .Select(x => new PaintingDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    CoverPhotoResourceDto = x.Galleries.Where(y => (y is ProfileGallery)).Select(y => new PhotoResourceDto() { Id = y.CoverPhoto.Id, Path = y.CoverPhoto.Path, Name = y.CoverPhoto.Name }).FirstOrDefault()
                })
                .ToList();
        }

        public List<PaintingCheckedDto> FindPaintingsForCheck(Guid exhibitionId)
        {
            //return new MultiCheckedDto<PaintingDto>()
            //{
            //    DtosToCheck = _modelContext.Set<Painting>().ToDictionary(x => new PaintingDto() { Id = x.Id, Title = x.Title }, y => false)

            //};

            return _modelContext.Set<Painting>()
                .Select(x => new PaintingCheckedDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    PhotoResourceDto = x.Galleries.Where(y => (y is ProfileGallery)).Select(y => new PhotoResourceDto() { Id = y.CoverPhoto.Id, Path = y.CoverPhoto.Path, Name = y.CoverPhoto.Name }).FirstOrDefault(),
                    Checked = x.Exhibitions.Where(y => y.Id == exhibitionId).FirstOrDefault() != null
                })
                .ToList();
            //_modelContext.Set<Painting>()
            //    .Select(x => new MultiCheckedDto<PaintingDto>()
            //    {
            //        Dtos = 
            //    });
            //return _modelContext.Set<Painting>()
            //    .Select(x => new CheckedDto<PaintingDto>() { Checked = false, Dto = new PaintingDto() { Id = x.Id, Title = x.Title } })
            //    .ToList();
            //.SingleOrDefault();
        }
    }
}
