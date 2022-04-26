<?php

$servername = "docker-db-1";

$dbname = "SoftSensConf";

$username = "unity";

$password = "Password1";

$name = $amount = "";

if($_SERVER["REQUEST_METHOD"] == "GET"){

		$conn = new mysqli($servername, $username, $password, $dbname);

		if ($conn->connect_error) {
			die("Connection failed: " . $conn->connect_error);
		}
		$name = test_input($_GET["name"]);
		if (isset($_GET["amount"])){
			$amount = test_input($_GET["amount"]);
			$sql = "SELECT UNIX_TIMESTAMP(Timestamp), ValueRaw, ValueScaled, AL, Ah FROM (SELECT * FROM Measurement WHERE InstrumentName LIKE '" . $name . "' ORDER BY Timestamp DESC Limit " . $amount . ") sub ORDER BY Timestamp ASC";
		}
		else{
			$sql = "SELECT UNIX_TIMESTAMP(Timestamp), ValueRaw, ValueScaled, AL, Ah FROM Measurement WHERE InstrumentName LIKE '" . $name . "'";
		}
		

		if ($result = $conn->query($sql)) {
			while ($row = $result->fetch_assoc()) {
				$row_timestamp = $row["UNIX_TIMESTAMP(Timestamp)"];
				$row_valueRaw = $row["ValueRaw"];
        $row_valueScaled = $row["ValueScaled"];
				$row_al = $row["AL"];
				$row_ah = $row["Ah"];
				echo $row_timestamp . ",";
				echo $row_valueRaw . ",";
				echo $row_valueScaled . ",";	
        echo $row_al . ",";
				echo $row_ah . ";";
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
