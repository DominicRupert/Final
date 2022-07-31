import { AppState } from "../AppState.js";
import { logger } from "../utils/logger.js";
import { api } from "./AxiosService.js";



class KeepsService{

    async getKeeps(){
        try{
            const res = await api.get('api/keeps');
            AppState.events=res.data;
        }catch(error){
            logger.error(error);
        }
    }

}


export const keepsService = new KeepsService();