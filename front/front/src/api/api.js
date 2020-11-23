import Axios from 'axios'

const axios = Axios.create({
    baseURL: 'http://localhost:5000'
})

export default class API {
    static async getMessage(id){
        const resp = await axios.get(`/api/chat/${id}`)
        return resp.data
    }

    static async sendMessage(username, text) {
        await axios.post( `/api/chat`, {
            username,
            text,
        })
    }
}