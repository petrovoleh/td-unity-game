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
    FOREIGN KEY (map_id) REFERENCES Map(map_id)
);


CREATE TABLE Completed_challenge
(
    username VARCHAR(30),
    challenge_id INTEGER,
    difficulty INTEGER,
    beat_id VARCHAR(35)UNIQUE PRIMARY KEY,
    FOREIGN KEY (username) REFERENCES Player(username)
    FOREIGN KEY (challenge_id) REFERENCES Map(challenge_id)
);


insert INTO Map
values(1);
insert INTO Map
values(2);

insert INTO Challenge
values(1, 1);
insert INTO Challenge
values(2, 2);
