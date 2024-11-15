﻿create Database BlogsDB
Use BlogsDB
--Hocam Data-lari insert etmek isine vaxt xerclememek ucun ChatGpt den istifade etmisem.
Create table Catagories
(
ID int Primary Key Identity,
[Name] nvarchar(60) not null unique
)
INSERT INTO Catagories(Name) VALUES
('Technology'),
('Sports'),
('Health'),
('Education'),
('Travel');

Create table Tags
(
ID int Primary Key Identity,
[Name] nvarchar(60) not null unique
)
INSERT INTO Tags([Name]) VALUES
('Software'),
('Football'),
('Healthy Living'),
('Education'),
('Holiday'),
('Travel'),
('Mobile'),
('Growth'),
('Technology'),
('Exercise');


Create table Users
(
ID int Primary Key Identity,
UserName nvarchar(60) not null unique,
FullName nvarchar(60) not null ,
Age int not null, check(Age>0 and Age<150) 
)
INSERT INTO Users(UserName, FullName, Age) VALUES
('ali_kaya', 'Ali Kaya', 30),
('ayse_dogan', 'Ayşe Doğan', 25),
('mehmet_yilmaz', 'Mehmet Yılmaz', 35),
('zeynep_ozdemir', 'Zeynep Özdemir', 28),
('burak_aydin', 'Burak Aydın', 40);


Create table Blogs 
(
ID int Primary Key Identity,
Title nvarchar(50) not null unique,
[Description] nvarchar(100) not null,
UserID int Foreign key references Users(ID),
CategoriesID int Foreign key references Catagories(ID)
)



INSERT INTO Blogs (Title, [Description], UserID, CategoriesID) VALUES
('Technology and the Future', 'An exploration of the future of technology.', 1, 1),
('The Basics of Football', 'The history and basic rules of football.', 2, 2),
('Healthy Living Tips', 'Daily tips for a healthier lifestyle.', 3, 3),
('New Education Methods', 'The latest innovations and modern methods in education.', 4, 4),
('Travel Guide: Turkey', 'Explore the best vacation spots in Turkey.', 5, 5);


Create table BlogTags
(
ID int primary key identity,
BlogsID int foreign key references Blogs(ID),
TagsID int foreign key references Tags(ID)	
)
INSERT INTO BlogTags (BlogsID, TagsID) VALUES
(1, 1),  -- "Technology and the Future" blog, "Software" tag
(1, 9),  -- "Technology and the Future" blog, "Technology" tag
(2, 2),  -- "The Basics of Football" blog, "Football" tag
(3, 3),  -- "Healthy Living Tips" blog, "Healthy Living" tag
(3, 8),  -- "Healthy Living Tips" blog, "Exercise" tag
(4, 4),  -- "New Education Methods" blog, "Education" tag
(5, 5),  -- "Travel Guide: Turkey" blog, "Holiday" tag
(5, 6),  -- "Travel Guide: Turkey" blog, "Travel" tag
(5, 7);  -- "Travel Guide: Turkey" blog, "Mobile" tag


Create table Comments
(
ID int Primary Key Identity,
Content  nvarchar(250) not null unique,
UserID int Foreign key references Users(ID),
BlogsID  int Foreign key references Blogs(ID)
)
INSERT INTO Comments (Content, UserID, BlogsID) VALUES
('This post is very interesting! I am looking forward to more posts about the future of technology.', 2, 1),
('It would be great if you added more details about football.', 3, 2),
('The tips on healthy living are really helpful.', 4, 3),
('The new education methods are truly innovative.', 1, 4),
('Your travel guide to Turkey is very informative.', 5, 5);

Select * from Users
Select * from Catagories
Select * from Comments
Select * from BlogTags
Select * from Tags
Select * from Blogs





--1. Blogs'un Title, Users'in userName ve fullName columnlarini qaytaran view yazirsiniz.

create view InfoViewBloger
as
select
b.Title as 'Blog Titles',
u.UserName as  'Bloger User Name',
u.FullName as 'Bloger Full Name' 
from Users as u
join Blogs as b
on u.ID = b.UserID

select * from InfoViewBloger

	
--2. Blogs'un Title, Categories'in Name columnlarini qaytaran view.
create view BlogsAndCatagoryNames
as
select 
b.Title as 'Blog Titles',
c.Name as 'Catagory Names'
from Blogs as b
join Catagories as c
on b.CategoriesID =c.ID

select * from BlogsAndCatagoryNames


--3. @userId parametri qebul edib hemin parametre uygun userin yazdigi commentleri qaytaran procedure yazirsiniz. 5
Create Procedure usp_get_comment_content @userId int
as
select u.ID as 'User Id',u.FullName as 'User Full Name',c.Content as 'Comment content' from Comments as c
join Users as u
on c.UserID = u.ID
where u.ID = @userId

exec usp_get_comment_content 2

--4. @userId parametri qebul edib hemin parametre uygun userin bloglarini qaytaran procedure yazirsiniz. 5
create Procedure usp_get_user_blogs @userId int 
as
select
u.ID as 'User Id',
u.FullName as 'User Full Name',
b.Title as 'Blogs title'
from Users	as u
join Blogs as b
on u.ID = b.UserID
where @userId = u.ID

exec usp_get_user_blogs 4

--5. Parametr olaraq, @categoryId qebul edib, hemin parametre Bloglarin sayini geri qaytaran function yazirsiniz. 15
create function getCountBlogs(@categoryId int)
returns int
as
begin
declare @CountOfBlogs int
select  @CountOfBlogs = COUNT(*) from Users as u
join Blogs as b
on u.ID = b.UserID
where @categoryId = CategoriesID
return @CountOfBlogs 
end

select dbo.getCountBlogs(1) as 'Blogs Count'



--6. Parametr olaraq, @userId qebul edib, hemin user'in yaratdigi bloglari table kimi geri qaytaran function yazirsiniz. 30

Create Function  GetBlogTable(@userId INT)
Returns Table
as
return(
select
b.ID as BlogId, 
b.Title as BlogTitle,
b.Description as BlogDescription        
From Blogs b
Join Users u on u.ID = b.UserID
Where u.ID = @userId
)

select * from GetBlogTable(3)


