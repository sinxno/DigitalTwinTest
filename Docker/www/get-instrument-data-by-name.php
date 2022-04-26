<?php

$servername = "docker-db-1";

$dbname = "SoftSensConf";

$username = "unity";

$password = "Password1";

$name = "";

if($_SERVER["REQUEST_METHOD"] == "GET"){

		$conn = new mysqli($servername, $username, $password, $dbname);

		if ($conn->connect_error) {
			die("Connection failed: " . $conn->connect_error);
		}
		$name = test_input($_GET["name"]);
		$sql = "SELECT LRV, URV, AL_Value, AH_Value FROM Instrument WHERE Name LIKE '" . $name . "'";

		if ($result = $conn->query($sql)) {
			while ($row = $result->fetch_assoc()) {
				$row_lrv = $row["LRV"];
				$row_urv = $row["URV"];
				$row_al_value = $row["AL_Value"];
				$row_ah_value = $row["AH_Value"];
				echo $row_lrv . ",";
				echo $row_urv . ",";
				echo $row_al_value . ",";	
				echo $row_ah_value;
			}
			$result->free();
		}

		$conn->close();

	}

function test_input($data) {
	$data = trim($data);
	$data = stripslashes($data);
	$data = htmlspecialchars($data);
	return $data;
}
?>
