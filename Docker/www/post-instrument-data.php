<?php

$servername = "docker-db-1";

$dbname = "SoftSensConf";

$username = "unity";

$password = "Password1";

$api_key_value = "aaa";

$api_key = $name = $lrv = $urv = $al_value = $ah_value = "";

if ($_SERVER["REQUEST_METHOD"] == "POST"){
	$api_key = test_input($_POST["api_key"]);
	if($api_key == $api_key_value){
		$name = test_input($_POST["name"]);
		$lrv = test_input($_POST["lrv"]);
		$urv = test_input($_POST["urv"]);
		$al_value = test_input($_POST["al_value"]);
		$ah_value = test_input($_POST["ah_value"]);

		//Create connection
		$conn = new mysqli($servername, $username, $password, $dbname);

		//check connection
		if ($conn->connection_error){
			die("Connection failed: " . $conn->connection_error);
		}

		$sql = "INSERT INTO Instrument (Name, LRV, URV, AL_Value, AH_Value) VALUES ('" . $name . "', '" . $lrv . "', '" . $urv . "', '" . $al_value . "', '" . $ah_value . "')";
		
		if($conn->query($sql) === TRUE) {
			echo "New record created successfully";
		}
		else{
			echo "Error: " . $sql . "<br>" . $conn->error;
		}

		$conn->close();
	}
	else{
		echo "Wrong API Key provided.";
	}
}
else{
	echo "No data posted with HTTP POST.";
}

function test_input($data) {
	$data = trim($data);
	$data = stripslashes($data);
	$data = htmlspecialchars($data);
	return $data;
}

?>
