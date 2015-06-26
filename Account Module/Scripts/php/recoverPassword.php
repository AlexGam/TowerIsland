<?php
	require_once("config.php");

	$auth_host = $GLOBALS['auth_host'];
	$auth_user = $GLOBALS['auth_user'];
	$auth_pass = $GLOBALS['auth_pass'];
	$auth_dbase = $GLOBALS['auth_dbase'];

	$db = mysql_connect($auth_host, $auth_user, $auth_pass) or die (mysql_error());
	mysql_select_db($auth_dbase,$db);
	
	$email = mysql_real_escape_string($_POST['email']);

	$sql = mysql_query("SELECT * FROM account WHERE email = '$email'");
	$rows= mysql_num_rows($sql);

	if($rows < 1){
		echo "false";
	}else{
		$row = mysql_fetch_row($sql);
		$message = "Your password is:\n\n";
		 //Initialize the random password
  		$password = '';

  		//Initialize a random desired length
  		$desired_length = rand(8, 12);

  		for($length = 0; $length < $desired_length; $length++) {
    			//Append a random ASCII character (including symbols)
    			$password .= chr(rand(32, 126));
  		}

                $message .= $password;
                mail($email, 'Recover Password', $message, 'From: noreply@zerano-unity3d.com');
		$q = "UPDATE account SET password = MD5('".$password."') WHERE(email ='$email')";
    		$result_activate_account = mysql_query($q);

		echo "true";	
	}
	mysql_close($db);
?> 
 