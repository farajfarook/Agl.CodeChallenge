import Axios from "axios"

Axios.defaults.validateStatus = () => true

const settings: { default: ISettings, production: any } = {
    default: {
        dataApi: 'http://agl-developer-test.azurewebsites.net/people.json',
        aglApi: "http://localhost:8088/api/v1/",
        //baseUrl: "http://localhost:5000/",
    },
    production: {
        dataApi: 'http://agl-developer-test.azurewebsites.net/people.json',
        aglApi: "http://localhost:8088/api/v1/",
    },
}

export interface ISettings {
    dataApi: string
    aglApi: string
}

export const config = ({ ...settings.default, ...settings[process.env.TEST_ENV] } as ISettings)
