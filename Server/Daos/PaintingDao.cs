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

namespace Server.Daos
{
    public class PaintingDao : BaseDao
    {
        public PaintingDao(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public IPagedList<PaintingDto> FindPaged(BaseFilterDto baseFilterDto)
        {
            return _modelContext.Set<Painting>()
                .Where(ExpressionQueryBuilder.BuildWhere<Painting>(baseFilterDto))
                .OrderBy(x => x.Title)
                .Select(x => new PaintingDto() {
                    Id = x.Id,
                    Title = x.Title,
                    Technique = x.Technique,
                    Surface = x.Surface,
                    Width = x.Width,
                    Height = x.Height,
                    PhotoResourceDto = x.Galleries.Where(y => (y is ProfileGallery)).Select(y => new PhotoResourceDto() { Id = y.CoverPhoto.Id, Path = y.CoverPhoto.Path, Name = y.CoverPhoto.Name }).FirstOrDefault()
                })
                .ToPagedList(baseFilterDto.Page, baseFilterDto.PageSize);
        }

        public List<PaintingCheckedDto> FindPaintingsForCheck()
        {
            //return new MultiCheckedDto<PaintingDto>()
            //{
            //    DtosToCheck = _modelContext.Set<Painting>().ToDictionary(x => new PaintingDto() { Id = x.Id, Title = x.Title }, y => false)

            //};

            return _modelContext.Set<Painting>().Select(x => new PaintingCheckedDto() { Id = x.Id, Title = x.Title, Checked = false }).ToList();
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
