Feature: Pet Sort
    Pet Store Feature

    Scenario: Fetch grouped pets by owners gender, sorted by name
        Given the data API is working
        When calling the API to fetch grouped pets
        Then pets should be sorted by name
        And pets counts should be correct for "Male"
        And pets counts should be correct for "Female"
        And pets counts should be correct for "Other"
