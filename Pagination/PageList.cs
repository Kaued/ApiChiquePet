using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Pagination{

  public class PageList<T> : List<T>{
    public int CurrentPage{get; set;}
    public int TotalPages{get; set;}
    public int PageSize{get; set;}
    public int TotalCount{get; set;}

    public bool HasPrevious => CurrentPage>1;
    public bool HasNext => CurrentPage < TotalPages;

    public PageList(List<T> itens, int count, int pageNumber, int pageSize){
      TotalCount=count;
      PageSize = pageSize;
      CurrentPage = pageNumber;
      TotalPages = (int) Math.Ceiling(count / (double) pageSize);

      AddRange(itens); 
    }  
    

    public static PageList<T> ToPageList (IQueryable<T> source, int pageNumber, int pageSize){
      var count = source.Count();
      var itens = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

      return new PageList<T>(itens, count, pageNumber, pageSize);
    }

    public async static Task<PageList<T>> ToPageListAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
      var count = source.Count();
      var itens = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

      return new PageList<T>(itens, count, pageNumber, pageSize);
    }
  }
}