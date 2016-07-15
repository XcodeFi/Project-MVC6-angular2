using Graduation.Entities;
using Graduation.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Graduation.Infrastructure
{
    public class SampleData
    {
        const string defaultAdminUserName = "DefaultAdminUserName";
        const string defaultAdminPassword = "DefaultAdminPassword";
        static string UserId = string.Empty;

        private static GraduationDbContext _context;
        public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider, bool createUsers = true)
        {
            _context = (GraduationDbContext)serviceProvider.GetService(typeof(GraduationDbContext));
            if (createUsers)
            {
                await CreateAdminUser(serviceProvider);
                UserId = _context.Users.FirstOrDefault(u => u.UserName.Equals("Administrator@test.com")).Id;
                InsertTestData();
            }
        }
        private static void InsertTestData()
        {
            //them cate cha
            if (!_context.Categories.Any())
            {
                _context.Categories.AddRange(Cates.Select(c => c.Value));

                _context.SaveChanges();

                //them cate con
                var catesChild = GetCate(Cates);
                _context.Categories.AddRange(catesChild);
                _context.SaveChanges();
            }
            //buoc trung gian chuyen cate[] thanh dic cate

            List<Category> AllCate = _context.Categories.ToList();

            Dictionary<string, Category> cateDic = new Dictionary<string, Category>();
            foreach (Category item in AllCate)
            {
                cateDic.Add(item.Name, item);
            }

            if (!_context.Views.Any())
            {
                View _view = new View
                {
                    Key = 1,
                    TotalViewings = 0,
                    TotalViewIsMember = 0,
                    TotalViews = 0
                };
                _context.Views.Add(_view);
                _context.SaveChanges();
            }
            //add card vao csdl
            var cards = GetCard(cateDic);
            if (!_context.Cards.Any())
            {
                _context.Cards.AddRange(cards);
                _context.SaveChanges();
            }
        }

        // TODO [EF] This may be replaced by a first class mechanism in EF
        private static void AddOrUpdateAsync<TEntity>(
            IServiceProvider serviceProvider,
            Func<TEntity, object> propertyToMatch, IEnumerable<TEntity> entities)
            where TEntity : class
        {
            // Query in a separate context so that we can attach existing entities as modified
            List<TEntity> existingData;
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<GraduationDbContext>();
                existingData = db.Set<TEntity>().ToList();
            }

            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<GraduationDbContext>();
                foreach (var item in entities)
                {
                     db.Entry(item).State = existingData.Any(g => propertyToMatch(g).Equals(propertyToMatch(item)))
                        ? EntityState.Modified
                        : EntityState.Added;
                }

                db.SaveChangesAsync();
            }
        }


        /// <summary>
        /// Creates a store manager user who can manage the inventory.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            var env = serviceProvider.GetService<IHostingEnvironment>();

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            var configuration = builder.Build();

            //const string adminRole = "Administrator";

            
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            // TODO: Identity SQL does not support roles yet
            //var roleManager = serviceProvider.GetService<ApplicationRoleManager>();
            //if (!await roleManager.RoleExistsAsync(adminRole))
            //{
            //    await roleManager.CreateAsync(new IdentityRole(adminRole));
            //}

            var user = await userManager.FindByNameAsync(configuration[defaultAdminUserName]);
            if (user == null)
            {
                user = new ApplicationUser { UserName = configuration[defaultAdminUserName],Email= configuration[defaultAdminUserName] };
                await userManager.CreateAsync(user, configuration[defaultAdminPassword]);

                //await userManager.AddToRoleAsync(user, adminRole);
                await userManager.AddClaimAsync(user, new Claim("Manager", "Allowed"));
            }
        }


        private static Dictionary<string, Category> cates;
        /// <summary>
        /// Nut goc//cate cha
        /// </summary>
        public static Dictionary<string, Category> Cates
        {

            //Thiệp mới
            get
            {
                if (cates == null)
                {
                    var catesList = new Category[]
                    {
                        new Category {Name="Thiệp mới",Level=0, ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Ngày lễ",Level=0, ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Thiệp nhiều người gửi",Level=0, ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                    };
                    cates = new Dictionary<string, Category>();

                    foreach (Category cate in catesList)
                    {
                        cates.Add(cate.Name, cate);
                    }
                }
                return cates;
            }
        }
        /// <summary>
        /// them cate con, va gan vao cate cha
        /// </summary>
        /// <param name="cates">cate cha</param>
        /// <returns></returns>
        public static Category[] GetCate(Dictionary<string, Category> cates)
        {
            var catesList = new Category[]
            {
                        new Category {Name="Thiệp cưới",Icon="fa fa-glass fa-fw",CateParent=cates["Thiệp mới"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Thiệp cảm ơn",Icon="fa fa-mail-reply-all fa-fw",CateParent=cates["Thiệp mới"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Thiệp chúc mừng",Icon="fa fa-star-o fa-fw", CateParent=cates["Thiệp mới"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Thiệp mừng thành viên mới",Icon="fa fa-child fa-fw",CateParent=cates["Thiệp mới"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 06",CateParent=cates["Thiệp mới"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 07",CateParent=cates["Thiệp mới"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Mùng 10/3",Icon="fa fa-calendar fa-fw",CateParent=cates["Ngày lễ"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Quốc tế lao động 1/5",Icon="fa fa-calendar fa-fw",CateParent=cates["Ngày lễ"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Ngày quốc tế phụ nữ",Icon="fa fa-calendar fa-fw",CateParent=cates["Ngày lễ"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Phụ nữ Việt Nam",Icon="fa fa-calendar fa-fw",CateParent=cates["Ngày lễ"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 15",CateParent=cates["Ngày lễ"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Thiệp sinh nhật",Icon="fa fa-birthday-cake fa-fw",CateParent=cates["Thiệp nhiều người gửi"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Lễ tình yêu 14/2",Icon="fa fa-heart fa-fw",CateParent=cates["Thiệp nhiều người gửi"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Thiệp chúc mừng tốt nghiệp",Icon="fa fa-graduation-cap fa-fw",CateParent=cates["Thiệp nhiều người gửi"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Thiệp chúc mừng 1",Icon="fa fa-star-o fa-fw",CateParent=cates["Thiệp nhiều người gửi"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 25",CateParent=cates["Thiệp nhiều người gửi"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 26",CateParent=cates["Thiệp nhiều người gửi"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"}
            };
            foreach (var cate in catesList)
            {
                cate.ParentId = cate.CateParent.Id;
            }
            return catesList;
        }

        private static Card[] GetCard(Dictionary<string, Category> cates)
        {
            string content = "content abc";
            string imageUrl = "15074-purrfect.jpg";
            string imageUrl2 = "15073-miss-you-kittens.jpg";
            string imageUrl3 = "15049-great-job.jpg";
            string textSearch = "text1,text2,text3";
            

            var cards = new Card[]
            {
                new Card{Title="Card 1",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp cưới"],ApplycationUserId=UserId},
                new Card{Title="Card 2",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp cưới"],ApplycationUserId=UserId},
                new Card{Title="Card 3",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp cưới"],ApplycationUserId=UserId},
                new Card{Content=content,Title="Card 132",ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp cưới"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,Title="Card 113",TextSearch=textSearch,Category=cates["Thiệp cưới"],ApplycationUserId=UserId},
                new Card{Title="Card 4",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp cưới"],ApplycationUserId=UserId},
                new Card{Title="Card 5",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp cưới"],ApplycationUserId=UserId},
                new Card{Title="Card 6",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp cưới"],ApplycationUserId=UserId},
                new Card{Title="Card 7",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp cưới"],ApplycationUserId=UserId},
                new Card{Title="Card 8",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp cưới"],ApplycationUserId=UserId},
                new Card{Title="Card 9",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp cưới"],ApplycationUserId=UserId},
                new Card{Title="Card 10",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp cảm ơn"],ApplycationUserId=UserId},
                new Card{Title="Card 11",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp cảm ơn"],ApplycationUserId=UserId},
                new Card{Title="Card 12",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp cảm ơn"],ApplycationUserId=UserId},
                new Card{Title="Card 13",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp cảm ơn"],ApplycationUserId=UserId},
                new Card{Title="Card 14",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp cảm ơn"],ApplycationUserId=UserId},
                new Card{Title="Card 15",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp cảm ơn"],ApplycationUserId=UserId},
                new Card{Title="Card 16",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp cảm ơn"],ApplycationUserId=UserId},
                new Card{Title="Card 17",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp cảm ơn"],ApplycationUserId=UserId},
                new Card{Title="Card 18",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp cảm ơn"],ApplycationUserId=UserId},
                new Card{Title="Card 19",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp cảm ơn"],ApplycationUserId=UserId},
                new Card{Title="Card 20",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp chúc mừng"],ApplycationUserId=UserId},
                new Card{Title="Card 21",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp chúc mừng"],ApplycationUserId=UserId},
                new Card{Title="Card 22",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp chúc mừng"],ApplycationUserId=UserId},
                new Card{Title="Card 23",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp chúc mừng"],ApplycationUserId=UserId},
                new Card{Title="Card 24",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Thiệp chúc mừng"],ApplycationUserId=UserId},
                new Card{Title="Card 26",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Mùng 10/3"],ApplycationUserId=UserId},
                new Card{Title="Card 25",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Mùng 10/3"],ApplycationUserId=UserId},
                new Card{Title="Card 27",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Mùng 10/3"],ApplycationUserId=UserId},
                new Card{Title="Card 28",Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Mùng 10/3"],ApplycationUserId=UserId},
                new Card{Title="Card 29",Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Mùng 10/3"],ApplycationUserId=UserId},
                new Card{Title="Card 30",Content=content,ImageUrl=imageUrl2,TextSearch=textSearch,Category=cates["Mùng 10/3"],ApplycationUserId=UserId},
                new Card{Title="Card 31",Content=content,ImageUrl=imageUrl2,TextSearch=textSearch,Category=cates["Quốc tế lao động 1/5"],ApplycationUserId=UserId},
                new Card{Title="Card 33",Content=content,ImageUrl=imageUrl2,TextSearch=textSearch,Category=cates["Quốc tế lao động 1/5"],ApplycationUserId=UserId},
                new Card{Title="Card 32",Content=content,ImageUrl=imageUrl2,TextSearch=textSearch,Category=cates["Quốc tế lao động 1/5"],ApplycationUserId=UserId},
                new Card{Title="Card 34",Content=content,ImageUrl=imageUrl2,TextSearch=textSearch,Category=cates["Ngày quốc tế phụ nữ"],ApplycationUserId=UserId},
                new Card{Title="Card 35",Content=content,ImageUrl=imageUrl2,TextSearch=textSearch,Category=cates["Ngày quốc tế phụ nữ"],ApplycationUserId=UserId},
                new Card{Title="Card 36",Content=content,ImageUrl=imageUrl2,TextSearch=textSearch,Category=cates["Ngày quốc tế phụ nữ"],ApplycationUserId=UserId},
                new Card{Title="Card 37",Content=content,ImageUrl=imageUrl2,TextSearch=textSearch,Category=cates["Phụ nữ Việt Nam"],ApplycationUserId=UserId},
                new Card{Title="Card 38",Content=content,ImageUrl=imageUrl2,TextSearch=textSearch,Category=cates["Phụ nữ Việt Nam"],ApplycationUserId=UserId},
                new Card{Title="Card 39",Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Phụ nữ Việt Nam"],ApplycationUserId=UserId},
                new Card{Title="Card 40",Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Thiệp sinh nhật"],ApplycationUserId=UserId},
                new Card{Title="Card 41",Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Thiệp sinh nhật"],ApplycationUserId=UserId},
                new Card{Title="Card 42",Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Thiệp sinh nhật"],ApplycationUserId=UserId},
                new Card{Title="Card 43",Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Thiệp sinh nhật"],ApplycationUserId=UserId},
                new Card{Title="Card 44",Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Lễ tình yêu 14/2"],ApplycationUserId=UserId},
                new Card{Title="Card 45",Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Lễ tình yêu 14/2"],ApplycationUserId=UserId},
                new Card{Title="Card 46",Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Lễ tình yêu 14/2"],ApplycationUserId=UserId},
                new Card{Title="Card 47",Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Thiệp chúc mừng tốt nghiệp"],ApplycationUserId=UserId},
                new Card{Title="Card 48",Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Thiệp chúc mừng tốt nghiệp"],ApplycationUserId=UserId},
                new Card{Title="Card 49",Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Thiệp chúc mừng tốt nghiệp"],ApplycationUserId=UserId},
                new Card{Title="Card 50",Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Thiệp chúc mừng"],ApplycationUserId=UserId},
                new Card{Title="Card 51",Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Thiệp chúc mừng"],ApplycationUserId=UserId},
                new Card{Title="Card 52",Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Thiệp chúc mừng"],ApplycationUserId=UserId},
                new Card{Title="Card 53",Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Thiệp chúc mừng"],ApplycationUserId=UserId}
            };
            foreach (var card in cards)
            {
                card.CateId = card.Category.Id;
            }
            return cards;
        }
    }
}
