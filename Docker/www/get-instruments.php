<?php

$servername = "docker-db-1";

$dbname = "SoftSensConf";

$username = "unity";

$password = "Password1";

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
	die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT Name FROM Instrument ORDER BY Name";

if ($result = $conn->query($sql)) {
	while ($row = $result->fetch_assoc()) {
		$row_name = $row["Name"];

		echo $row_name, ";";
	}
	$result->free();
}

$conn->close();
?>
