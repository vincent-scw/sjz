package main

import "net/http"

// Route for route
type Route struct {
	Name        string
	Method      string
	Pattern     string
	HandlerFunc http.HandlerFunc
}

// Routes returns a list
type Routes []Route

var routes = Routes{
	Route{
		"Health",
		"GET",
		"/health",
		health,
	},
}
