CREATE TABLE Player
(
    username VARCHAR(30)UNIQUE PRIMARY KEY,
    password VARCHAR(30)
);

CREATE TABLE Map(
    map_id INTEGER UNIQUE PRIMARY KEY
);

CREATE TABLE Beaten_map
(
    username VARCHAR(30),
    map_id INTEGER,
    difficulty INTEGER,
    beat_id VARCHAR(35)UNIQUE PRIMARY KEY,
    FOREIGN KEY (username) REFERENCES Player(username),
    FOREIGN KEY (map_id) REFERENCES Map(map_id)
);

CREATE TABLE Challenge
(
    challenge_id INTEGER UNIQUE PRIMARY KEY,
    map_id INTEGER,
    title VARCHAR(30),
    task VARCHAR(30),
    task_data jsonb,
    FOREIGN KEY (map_id) REFERENCES Map(map_id)
);


CREATE TABLE Completed_challenge
(
    username VARCHAR(30),
    challenge_id INTEGER,
    difficulty INTEGER,
    beat_id VARCHAR(35)UNIQUE PRIMARY KEY,
    FOREIGN KEY (username) REFERENCES Player(username)
);


insert INTO Player
values('player1','123');
insert INTO Player
values('player2','1235');
insert INTO Player
values('player3','321');

insert INTO Beaten_map
values('player1', 01, 3,'player101');
insert INTO Beaten_map
values('player1', 02, 1,'player102');
insert INTO Beaten_map
values('player1', 03, 2,'player103');
insert INTO Beaten_map
values('player2', 01, 3,'player203');
insert INTO Beaten_map
values('player2', 02, 2,'player202');
insert INTO Beaten_map
values('player2', 05, 1,'player105');


insert INTO ChallengeId
values(01,01,"task1 ","without wizard", null  );
insert INTO ChallengeId
values(10,01,"task10 ","without wizard", null  );
insert INTO ChallengeId
values(08,01,"task08 ","without wizard", null  );
insert INTO ChallengeId
values(11,01,"task11 ","without wizard", null  );
insert INTO ChallengeId
values(06,06,"task1 ","without wizard", null  );
insert INTO ChallengeId
values(05,05,"task1 ","without wizard", null  );


insert INTO Completed_challenge
values('player1', 01, 3,'player103');
insert INTO Completed_challenge
values('player1', 10, 1,'player101');
insert INTO Completed_challenge
values('player1', 08, 2,'player108');
insert INTO Completed_challenge
values('player2', 11, 3,'player203');
insert INTO Completed_challenge
values('player2', 06, 2,'player206');
insert INTO Completed_challenge
values('player2', 05, 1,'player205');