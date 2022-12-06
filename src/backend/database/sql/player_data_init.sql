CREATE TABLE Player
(
    username VARCHAR(30)UNIQUE PRIMARY KEY,
    password VARCHAR(30),
    level FLOAT,
    reg_date DATE
);

CREATE TABLE Completed_map
(
    username VARCHAR(30),
    FOREIGN KEY (username) REFERENCES Player(username),
    map_id INTEGER,
    medals INTEGER,
    difficulty INTEGER
);

