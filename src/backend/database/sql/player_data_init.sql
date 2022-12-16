CREATE TABLE Player
(
    username VARCHAR(30)UNIQUE PRIMARY KEY,
    password VARCHAR(30),
    reg_date DATE,
    JWTkey VARCHAR(60)
);

CREATE TABLE Beaten_map
(
    username VARCHAR(30),
    FOREIGN KEY (username) REFERENCES Player(username),
    map_id INTEGER,
    difficulty INTEGER
);

CREATE TABLE Challenge
(
    challenge_id INTEGER UNIQUE PRIMARY KEY,
    map_id INTEGER,
    title VARCHAR(30),
    task VARCHAR(30),
    task_data jsonb
);


CREATE TABLE Completed_challenge
(
    username VARCHAR(30),
    challange_id INTEGER,
    FOREIGN KEY (username) REFERENCES Player(username),
    difficulty INTEGER
);


insert INTO Player
values('player1','123','10-10-2022',null);
insert INTO Player
values('player2','1235','09-10-2022',null);
insert INTO Player
values('player3','321','10-12-2022',null);

insert INTO Beaten_map
values('player1', 01, 3);
insert INTO Beaten_map
values('player1', 02, 1);
insert INTO Beaten_map
values('player1', 03, 2);
insert INTO Beaten_map
values('player2', 01, 3);
insert INTO Beaten_map
values('player2', 02, 2);
insert INTO Beaten_map
values('player2', 05, 1);


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
values('player1', 01, 3,);
insert INTO Completed_challenge
values('player1', 10, 1);
insert INTO Completed_challenge
values('player1', 08, 2);
insert INTO Completed_challenge
values('player2', 11, 3);
insert INTO Completed_challenge
values('player2', 06, 2);
insert INTO Completed_challenge
values('player2', 05, 1);