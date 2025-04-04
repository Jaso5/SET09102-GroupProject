CREATE OR REPLACE DATABASE Environment;
USE Environment;

CREATE OR REPLACE TABLE Sensors(
	id INT AUTO_INCREMENT,

	category VARCHAR(30),
	
	quantity VARCHAR(60),
	symbol VARCHAR(30),
	unit VARCHAR(30),
	unit_desc VARCHAR(60),
	
	frequency INT,
	safe_level FLOAT,
	
	ref VARCHAR(30),
	sensor VARCHAR(30),
	link VARCHAR(100),
	
	PRIMARY KEY(id)
);

CREATE OR REPLACE TABLE Readings(
	id INT AUTO_INCREMENT,

	ts TIMESTAMP NOT NULL,
	val FLOAT,
	sensor INT NOT NULL,
	
	CONSTRAINT `fk_sensor`
		FOREIGN KEY (sensor) REFERENCES Sensors(id)
		ON DELETE CASCADE
		ON UPDATE RESTRICT,

	PRIMARY KEY (id)
);