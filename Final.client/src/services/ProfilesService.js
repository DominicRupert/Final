import { AppState } from "../AppState.js";
import { api } from "./AxiosService.js";
import { logger } from "../utils/Logger.js";
import {keepsService} from "../services/KeepsService.js"

class ProfilesService{
    async getProfile(id){
        const res = await api.get('api/profiles/'+id);
        logger.log("[getProfile]", res.data);
        
        // await keepsService.getKeepsByUserId(id);
        AppState.activeProfile = res.data;


    }
   


}

export const profilesService = new ProfilesService();