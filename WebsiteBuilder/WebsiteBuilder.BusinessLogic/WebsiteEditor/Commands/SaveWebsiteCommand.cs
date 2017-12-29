using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteBuilder.BusinessLogic.Infrastructure;
using WebsiteBuilder.Core.Service;
using WebsiteBuilder.Public.Website;
using WebsiteBuilder.Public.WebsiteEditor;
using EntityFramework.Extensions;
using WebsiteBuilder.Public.NavigationBar;

namespace WebsiteBuilder.BusinessLogic.WebsiteEditor.Commands
{
    public class SaveWebsiteCommand : CommandBase, ICommand<OperationResult, SaveWebsiteEditorDto>
    {
        public OperationResult Execute(SaveWebsiteEditorDto request)
        {
            var website = Db.Websites
                .Single(x => x.Id == request.WebsiteId);

            website.Color = request.WebsiteColor;
            var addWebsiteContent = new List<WebsiteContentDto>();
            var updateWebsiteContent = new List<WebsiteContentDto>();
            var deleteNavigationBars = new List<NavigationBarDto>();
            deleteNavigationBars = request.IsDeleteNavigationArrayFromServer == true ? request.NavigationBarsToRemove : null;

            if (request.WebsiteContents != null)
            {
                addWebsiteContent = request.WebsiteContents.Where(x => x.Id == null).ToList();
                updateWebsiteContent = request.WebsiteContents.Where(x => x.Id != null).ToList();
            }


            if (request.WidgetsToRemove != null)
            {
                foreach (var item in request.WidgetsToRemove)
                {
                    if (item.WebsiteElementType == WebsiteElementType.Image)
                    {
                        var tmp = Db.WebsiteImages.First(x => x.Id == item.Id);
                        Db.WebsiteImages.Remove(tmp);
                    }
                    else
                    {
                        var tmp = Db.WebsiteTexts.First(x => x.Id == item.Id);
                        Db.WebsiteTexts.Remove(tmp);
                    }
                }
                Db.SaveChanges();
            }
            
            if(updateWebsiteContent.Count > 0)
            {
                foreach (var item in updateWebsiteContent)
                {
                    if (item.ImageSrc != null)
                    {
                        var tmp = Db.WebsiteImages.First(x => x.Id == item.Id);
                        tmp.Height = item.Height;
                        tmp.Width = item.Width;
                        tmp.CoordinateX = item.X;
                        tmp.CoordinateY = item.Y;
                    }
                    else if (item.Text != null)
                    {
                        var tmp = Db.WebsiteTexts.First(x => x.Id == item.Id);
                        tmp.Height = item.Height;
                        tmp.Width = item.Width;
                        tmp.CoordinateX = item.X;
                        tmp.CoordinateY = item.Y;
                        tmp.Text = item.Text;
                        tmp.FontSize = item.FontSize;
                        tmp.FontStyle = item.FontStyle;
                        tmp.FontWeight = item.FontWeight;
                        tmp.FontFamily = item.FontFamily;
                        tmp.FontColor = item.FontColor;
                    }

                }
                Db.SaveChanges();
            }

            if(addWebsiteContent.Count > 0)
            {
                foreach (var content in addWebsiteContent)
                {
                    if (content.WebsiteElementType == WebsiteElementType.Text)
                    {
                        var textEnitity = new Data.Entities.WebsiteText()
                        {
                            WebsiteId = request.WebsiteId,
                            CoordinateX = content.X,
                            CoordinateY = content.Y,
                            Height = content.Height,
                            Width = content.Width,
                            Text = content.Text,
                            SelectorId = content.SelectorId,
                            FontSize = content.FontSize,
                            FontFamily = content.FontFamily,
                            FontWeight = content.FontWeight,
                            FontStyle = content.FontStyle,
                            FontColor = content.FontColor
                        };
                        Db.WebsiteTexts.Add(textEnitity);
                    }
                    else
                    {
                        var imageEnitity = new Data.Entities.WebsiteImage()
                        {
                            CoordinateX = content.X,
                            CoordinateY = content.Y,
                            Height = content.Height,
                            Width = content.Width,
                            ImageId = content.ImageId,
                            SelectorId = content.SelectorId,
                            
                        };
                        Db.WebsiteImages.Add(imageEnitity);
                    }

                }
                Db.SaveChanges();
            }

            if(deleteNavigationBars != null)
            {
                foreach (var navigationBar in deleteNavigationBars)
                {
                    var entity = Db.WebsiteNavigationBar.First(x => x.Id == navigationBar.NavigationId);
                    Db.WebsiteNavigationBar.Remove(entity);
                }
                Db.SaveChanges();
            }

            var enitityNavigationBars = new List<Data.Entities.WebsiteNavigationBar>();
            if (request.NavigationBars != null)
            {
                foreach (var navigationBar in request.NavigationBars.Where(x => x.NavigationId == null))
                {
                    var entity = new Data.Entities.WebsiteNavigationBar()
                    {
                        NavigationName = navigationBar.NavigationName,
                        SectionName = navigationBar.SectionName,
                        WebsiteId = request.WebsiteId,
                        NavigationBarStyle = request.NavigationBarStyle
                    };
                    enitityNavigationBars.Add(entity);
                }
                Db.WebsiteNavigationBar.AddRange(enitityNavigationBars);
                Db.SaveChanges();
            }

            return new OperationResult();
        }
    }
}
