using AutoMapper;
using BookStore.Books.Api.Application.BookFeatures.Commands;
using BookStore.Books.Api.Application.BookFeatures.Queries;
using BookStore.Books.Api.Application.ModelsDto;
using BookStore.Books.Api.Models;
using BookStore.Books.Api.Persistence;
using BookStore.Books.Api.Test.Helpers;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BookStore.Books.Api.Test
{
    public class BookServiceTest
    { 
        private IEnumerable<Book> GetDataTest()
        {
            //this is data with GenFu
            A.Configure<Book>()
                .Fill(x => x.Title).AsArticleTitle()
                .Fill(x => x.AuthoBook, () => { return Guid.NewGuid(); });

            var listBookDto = A.ListOf<Book>(30);
            listBookDto[0].BookId = Guid.Empty;

            return listBookDto;
        }

        private Mock<ContextBook> CreateContext()
        {
            var dataTest = GetDataTest().AsQueryable();
            var dbSet = new Mock<DbSet<Book>>();
            dbSet.As<IQueryable<Book>>().Setup(x => x.Provider).Returns(dataTest.Provider);
            dbSet.As<IQueryable<Book>>().Setup(x => x.Expression).Returns(dataTest.Expression);
            dbSet.As<IQueryable<Book>>().Setup(x => x.ElementType).Returns(dataTest.ElementType);
            dbSet.As<IQueryable<Book>>().Setup(x => x.GetEnumerator()).Returns(dataTest.GetEnumerator());

            dbSet.As<IAsyncEnumerable<Book>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<Book>(dataTest.GetEnumerator()));


            dbSet.As<IQueryable<Book>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<Book>(dataTest.Provider));

            var context = new Mock<ContextBook>();
            context.Setup(x => x.Book).Returns(dbSet.Object);

            return context;
        }

        [Fact]
        public async void GetBookByID()
        {
            var mockContext = CreateContext();
            var mapConfig = new MapperConfiguration(cfg =>
           {
               cfg.AddProfile(new MappingTest());
           });

            var mapper = mapConfig.CreateMapper();

            //instance of request class
            var request = new BookByIdQuery();
            request.BookId = Guid.Empty;

            //instance of handler class
            var handler = new BookByIdQueryHandler(mockContext.Object, mapper);

            var book = await handler.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(book);
            Assert.True(book.BookId == Guid.Empty);
        }

        [Fact]
        public async void GetBooks()
        {
            //System.Diagnostics.Debugger.Launch();
            /**
             * Que me todo dentro del microservicio se encarga de realizar la consulta 
             * de libros de la base de datos?
             */

            /**
             * 1. Emular a la instancia de Entity Framework Core ContextBook
             * para emular las acciones y eventos de un objeto en un ambiente unit test
             * utilizando objetos de tipo mock.
             */

            var mockContext =  CreateContext();

            /**
             * 2. Emular a IMapper.
             */

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingTest()));
            var mapper = mapperConfig.CreateMapper();
            /**
             * 3. Instanciar la clase AllBooksListQueryHandler y pasarle los mocks creados
             */

            AllBooksListQueryHandler handler = new AllBooksListQueryHandler(mockContext.Object, mapper);
            AllBooksListQuery executeQuery = new AllBooksListQuery();

            var lstBooks = await handler.Handle(executeQuery, new System.Threading.CancellationToken());

            Assert.True(lstBooks.Any());
        }

        [Fact]
        public async void SaveBook()
        {
            System.Diagnostics.Debugger.Launch();

            var opt = new DbContextOptionsBuilder<ContextBook>()
                .UseInMemoryDatabase(databaseName: "BooksDB")
                .Options;

            var context = new ContextBook(opt);
            var request = new CreateBookCommand();
            request.Author = Guid.Empty;
            request.Title = "Libro Microservicios";
            request.PublicationDate = DateTime.Now;

            var handler = new CreateBookCommandHandler(context);

            var createdBook = await handler.Handle(request, new System.Threading.CancellationToken());

            Assert.True(createdBook != null);


        }
    }
}
