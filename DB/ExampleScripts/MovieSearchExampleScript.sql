DECLARE @FreeText NVARCHAR(100) ='Star';
DECLARE @BudgetMin BIGINT = 10000;
--DECLARE @BudgetMax BIGINT;
DECLARE @ActorPersonName VARCHAR(500) = 'Ford';
DECLARE @CrewPersonName VARCHAR(500) = 'Lucas';
--DECLARE @GenreName VARCHAR(100) = '';


SELECT *, COUNT(*) over() as TotalCount FROM (
SELECT DISTINCT m.*
  FROM [movies].[dbo].[movie] m
  INNER JOIN [dbo].[movie_cast] mcast ON m.movie_id = mcast.movie_id
  INNER JOIN [dbo].[person] a ON mcast.person_id = a.person_id
  INNER JOIN [dbo].[movie_crew] mcr ON m.movie_id = mcr.movie_id
  INNER JOIN [dbo].[person] c ON mcr.person_id = c.person_id
  INNER JOIN [dbo].[movie_genres] mg ON m.movie_id = mg.movie_id
  INNER JOIN [dbo].[genre] g ON mg.genre_id = g.genre_id
  WHERE  
  (FREETEXT([title], @FreeText) OR FREETEXT([overview], @FreeText) OR FREETEXT([tagline], @FreeText))
  --AND m.budget BETWEEN @BudgetMin AND @BudgetMax
  --AND m.budget > @BudgetMin
  AND m.budget < @BudgetMax
  AND a.person_name LIKE '%' + @ActorPersonName + '%'
  AND c.person_name LIKE '%' + @CrewPersonName + '%'
  --AND g.genre_name = @GenreName
  ) as T
  ORDER BY title 
  OFFSET 2 ROWS
  FETCH NEXT 2 ROWS ONLY;