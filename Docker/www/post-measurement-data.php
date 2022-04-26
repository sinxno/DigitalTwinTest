<?php

$servername = "docker-db-1";

$dbname = "SoftSensConf";

$username = "unity";

$password = "Password1";

$api_key_value = "bbb";

$api_key = $instrumentName = $valueRaw = $valueScaled = $al = $ah = "";
//echo phpinfo(INFO_VARIABLES);
//echo (var_dump($_POST);
if ($_SERVER['REQUEST_METHOD'] == "POST"){
	$api_key = test_input($_POST["api_key"]);
	if($api_key == $api_key_value){
		$instrumentName = test_input($_REQUEST["instrumentName"]);
		$valueRaw = test_input($_REQUEST["valueRaw"]);
		$valueScaled = test_input($_REQUEST["valueScaled"]);
		$al = test_input($_REQUEST["al"]);
		$ah = test_input($_REQUEST["ah"]);

		//Create connection
		$conn = new mysqli($servername, $username, $password, $dbname);

		//check connection
		if ($conn->connect_error){
			die("Connection failed: " . $conn->connection_error);
		}

		$sql = "INSERT INTO Measurement (InstrumentName, ValueRaw, ValueScaled, AL, AH) VALUES ('" . $instrumentName . "', '" . $valueRaw . "', '" . $valueScaled . "', '" . $al . "', '" . $ah . "')";
		
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
