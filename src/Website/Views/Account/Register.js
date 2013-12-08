$(function() {
			$("#registrationForm").validate(
			{
			    errorClass: "ui-state-error",
			    errorElement: "div",
        		rules: {
        			"username": { required: true, maxlength: 100, remote: {url: usernameUrl, type: "post"} },
        			"email": { required: true, maxlength: 100, email: true },
        			"password": { required: true, maxlength: 100, minlength: passwordLength },
        			"confirmPassword": { equalTo: "#txtPassword" },
        			"artistName": { maxlength: 100, remote: { url: artistNameUrl, type: "post", data:{artistId: newArtistId} } }
        		},
        		messages: {
        		    "username": { remote: "Sorry, but that Username is already taken" },
        			"email": {email : "Please enter a valid email address"},
        			"password": {minlength: "Password must at be at least " + passwordLength + " characters long"},
        			"confirmPassword": {equalTo: "Password doesn't match"},
        			"artistName": {remote: "Sorry, but that artist name is already taken"}
        		}
        	});

//        	$("#ajax-fc-container").captcha({
//        		formId: "registrationForm",
//        		captchaDir: "/content/captcha",
//        		url: captchaUrl
//        	});

		});