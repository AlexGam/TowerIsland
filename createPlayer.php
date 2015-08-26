<?php
	require_once("config.php");

	$auth_host = $GLOBALS['auth_host'];
	$auth_user = $GLOBALS['auth_user'];
	$auth_pass = $GLOBALS['auth_pass'];
    $auth_port = $GLOBALS['auth_port'];

    $strConn = "host=$auth_host port=$auth_port dbname=$auth_dbase user=$auth_user password=$auth_pass";
	
    $db = pg_connect($strConn) or die ("Error connection: " .pg_last_error());
	
	$account = pg_escape_string($_POST['account']);
	$name = pg_escape_string($_POST['name']);
	$custom = pg_escape_string($_POST['custom']);
	$level = pg_escape_string($_POST['level']);

	$sql = pg_query("SELECT * FROM player WHERE name = '$name'");
	$rows= pg_num_rows($sql);

	if($rows > 0){
		echo "false"; //Player already exists
	}else{
		pg_query("INSERT INTO player(name,custom,level,account) VALUES ('$name','$custom','$level','$account')");
		echo "true";	
	}
	pg_close($db);
?> 
 