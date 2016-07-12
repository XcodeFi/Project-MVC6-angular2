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
                UserId= _context.Users.FirstOrDefault(u => u.UserName == defaultAdminUserName).ToString();
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
            get
            {
                if (cates == null)
                {
                    var catesList = new Category[]
                    {
                        new Category {Name="Cate 0",Level=0, ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 1",Level=0, ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 2",Level=0, ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 3",Level=0, ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
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
                        new Category {Name="Cate 01",CateParent=cates["Cate 0"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 02",CateParent=cates["Cate 0"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 03",CateParent=cates["Cate 0"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 05",CateParent=cates["Cate 0"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 06",CateParent=cates["Cate 0"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 07",CateParent=cates["Cate 0"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 11",CateParent=cates["Cate 1"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 12",CateParent=cates["Cate 1"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 13",CateParent=cates["Cate 1"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 14",CateParent=cates["Cate 1"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 15",CateParent=cates["Cate 1"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 21",CateParent=cates["Cate 2"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 22",CateParent=cates["Cate 2"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 23",CateParent=cates["Cate 2"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 24",CateParent=cates["Cate 2"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 25",CateParent=cates["Cate 2"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"},
                        new Category {Name="Cate 26",CateParent=cates["Cate 2"],ImageUrl="15049-great-job.jpg",Description="No description description description description",UrlSlug="Na"}
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
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 01"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 01"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 01"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 01"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 01"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 01"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 01"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 01"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 01"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 01"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 01"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 01"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 01"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 01"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 01"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 01"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 01"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 01"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 01"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 01"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 01"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 11"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 11"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 11"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 11"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 11"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 11"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 24"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 21"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl,TextSearch=textSearch,Category=cates["Cate 21"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Cate 23"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl2,TextSearch=textSearch,Category=cates["Cate 22"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl2,TextSearch=textSearch,Category=cates["Cate 22"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl2,TextSearch=textSearch,Category=cates["Cate 22"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl2,TextSearch=textSearch,Category=cates["Cate 22"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl2,TextSearch=textSearch,Category=cates["Cate 22"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl2,TextSearch=textSearch,Category=cates["Cate 22"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl2,TextSearch=textSearch,Category=cates["Cate 22"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl2,TextSearch=textSearch,Category=cates["Cate 22"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl2,TextSearch=textSearch,Category=cates["Cate 22"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Cate 23"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Cate 23"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Cate 23"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Cate 23"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Cate 23"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Cate 23"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Cate 23"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Cate 23"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Cate 13"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Cate 13"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Cate 23"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Cate 13"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Cate 23"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Cate 23"],ApplycationUserId=UserId},
                new Card{Content=content,ImageUrl=imageUrl3,TextSearch=textSearch,Category=cates["Cate 23"],ApplycationUserId=UserId}
            };
            foreach (var card in cards)
            {
                card.CateId = card.Category.Id;
            }
            return cards;
        }
    }
}
