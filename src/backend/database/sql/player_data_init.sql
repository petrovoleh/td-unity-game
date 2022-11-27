CREATE TABLE Player
(
    username VARCHAR(30)UNIQUE PRIMARY KEY,
    user_password VARCHAR(30),
    player_level FLOAT,
    reg_date DATE
);

CREATE TABLE Completed_map
(
    FOREIGN KEY (username) REFERENCES Players(username)
    map_id INTEGER(30),
    challenges INTEGER(30),
    difficulty INTEGER,
);

