import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:7103/api/'
});

export default api;