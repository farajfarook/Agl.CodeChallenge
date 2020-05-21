import { Given, When, Then } from 'cucumber'
import Axios from 'axios';
import { expect } from 'chai'
import { config } from '../config';

var groupedPets = []
var rawData = []

Given('the data API is working', async function () {
    let resp = await Axios.get(config.dataApi)
    expect(resp.status).to.be.eql(200)
    rawData = resp.data
});

When('calling the API to fetch grouped pets', async function () {
    const resp = await Axios.get(`${config.aglApi}pets/_by_gender`)
    expect(resp.status).to.be.eql(200)
    groupedPets = resp.data.records
});

Then('pets counts should be correct for {string}', function (gender: string) {
    let count: number = 0
    for (const person of rawData.filter(p => p.gender == gender)) {
        count += person.pets && person.pets.length || 0
    }
    let groupedCount = groupedPets.find(g => g.gender == gender).pets.length
    expect(count, groupedCount)
});

Then('pets should be sorted by name', function () {
    for (const group of groupedPets) {
        const fetchedPets = [...group.pets]
        fetchedPets.sort((p1, p2) => {
            if (p1.name < p2.name) return -1
            else if (p1.name > p2.name) return 1
            else return 0
        })
        for (let i = 0; i < fetchedPets.length; i++) {
            expect(fetchedPets[i], group.pets[i])
        }
    }
});