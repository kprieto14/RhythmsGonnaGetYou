CREATE TABLE "Artists"(
    "Id" SERIAL PRIMARY KEY,
    "Name" TEXT NOT NULL,
    "CountryOfOrigin" TEXT,
    "NumberOfMembers" INT,
    "Website" TEXT,
    "Style" TEXT,
    "IsSigned" BOOLEAN,
    "ContactName" TEXT,
    "ContactPhoneNumber" TEXT
    );

CREATE TABLE "Albums"(
    "Id" SERIAL PRIMARY KEY,
    "Title" TEXT NOT NULL,
    "IsExplicit" BOOLEAN,
    "ReleaseDate" TEXT
    );

CREATE TABLE "Songs"(
    "Id" SERIAL PRIMARY KEY,
    "TrackNumber" TEXT,
    "Title" TEXT NOT NULL,
    "Duration" TEXT
    )
    


ALTER TABLE "Albums" ADD COLUMN "ArtistId" INTEGER NULL REFERENCES "Artist" ("Id");

ALTER TABLE "Songs" ADD COLUMN "AlbumId" INTEGER NULL REFERENCES "Albums" ("Id");

INSERT INTO "Artists" ("Name", "CountryOfOrigin", "NumberOfMembers", "Website", "Style", "IsSigned", "ContactName", "ContactPhoneNumber")
VALUES ('NSYNC', 'USA', '5', 'https://nsynconline.com', 'Pop', 'FALSE', 'Christine', '555-867-5309');

INSERT INTO "Albums" ("Title", "IsExplicit", "ReleaseDate")
VALUES ('N Sync', 'FALSE', '03/24/1998');

UPDATE "Artists" SET "AlbumId" = 1 WHERE "Id" IN (1);


INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('1', "Tearin' Up My Heart", '4:47', '1');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('2', 'You Got It', '3:32', '1');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('3', 'Sailing', '4:36', '1');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('4', 'Crazy For You', '3:41', '1');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('5', 'Riddle', '3:40', '1');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('6', 'For the Girl Who Has Everything', '3:51', '1');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('7', 'I Need Love', '3:14', '1');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('8', 'Giddy Up', '4:09', '1');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('9', 'Here We Go', '3:35', '1');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('10', 'Best of My Life', '4:46', '1');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('11', 'More Than a Feeling', '3:42', '1');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('12', 'I Want You Back', '4:24', '1');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('13', 'Together Again', '4:11', '1');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('14', 'Forever Young', '4:09', '1');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('1', 'Home for Christmas', '4:28', '2');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('2', 'Under My Tree', '4:32', '2');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('3', 'I Never Knew the Meaning of Christmas', '4:45', '2');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('4', 'Merry Christmas, Happy Holidays', '4:12', '2');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('5', 'The Christmas Song', '3:16', '2');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('6', 'I Guess It''s Christmas Time', '3:52', '2');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('7', 'All I Want Is You This Christmas', '3:43', '2');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('8', 'The First Noel', '3:28', '2');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('9', 'In Love on Christmas', '4:06', '2');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('10', 'It''s Christmas', '4:29', '2');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('11', 'O Holy Night', '3:33', '2');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('12', 'Love''s In Our Hearts On Christmas Day', '3:54', '2');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('13', 'The Only Gift', '3:51', '2');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('14', 'Kiss Me at Midnight', '3:28', '2');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('1', 'Bye Bye Bye', '3:19', '3');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('2', 'It''s Gonna Be Me', '3:11', '3');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('3', 'Space Cowboy (Yippie-Yi-Yay)', '4:21', '3');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('4', 'Just Got Paid', '4:08', '3');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('5', 'It Makes Me Ill', '3:26', '3');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('6', 'This I Promise You', '4:43', '3');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('7', 'No Strings Attached', '3:50', '3');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('8', 'Digital Get Down', '4:23', '3');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('9', 'Bringin'' da Noise', '3:30', '3');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('10', 'That''s When I''ll Stop Loving You', '4:50', '3');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('11', 'I''ll Be Good for You', '3:56', '3');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('12', 'I Thought She Knew', '3:20', '3');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('1', 'Pop', '3:57', '4');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('2', 'Celebrity', '3:17', '4');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('3', 'The Game Is Over', '3:25', '4');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('4', 'Girlfriend', '4:13', '4');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('5', 'The Two of Us', '3:50', '4');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('6', 'Gone', '4:51', '4');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('7', 'Tell Me, Tell Me... Baby', '3:36', '4');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('8', 'Up Against the Wall', '3:36', '4');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('9', 'See Right Through You', '2:52', '4');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('10', 'Selfish', '4:19', '4');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('11', 'Just Don''t Tell Me That', '3:02', '4');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('12', 'Something Like You', '4:14', '4');

INSERT INTO "Songs" ("TrackNumber", "Title", "Duration", "AlbumId")
VALUES ('13', 'Do Your Thing', '4:19', '4');





INSERT INTO "Albums" ("Title", "IsExplicit", "ReleaseDate")
VALUES ('Home for Christmas', 'FALSE', '11/10/1998');

INSERT INTO "Albums" ("Title", "IsExplicit", "ReleaseDate")
VALUES ('No Strings Attached', 'FALSE', '03/21/2000');

INSERT INTO "Albums" ("Title", "IsExplicit", "ReleaseDate")
VALUES ('Celebrity', 'FALSE', '07/24/2001');

