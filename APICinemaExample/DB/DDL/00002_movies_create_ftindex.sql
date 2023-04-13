--DROP FULLTEXT INDEX ON [dbo].[movie];
--DROP FULLTEXT CATALOG movie_catalog;

CREATE FULLTEXT CATALOG movie_catalog; 
CREATE FULLTEXT INDEX ON [dbo].[movie]  
 (   
  [title] Language 1033,  
  [overview] Language 1033,  
  [tagline] Language 1033       
 )   
 KEY INDEX PK__movie__83CDF7493EBEB0C9 ON movie_catalog; 