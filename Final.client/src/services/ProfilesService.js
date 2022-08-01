import { AppState } from "../AppState.js";
import { api } from "./AxiosService.js";
import {keepsService} from "../services/KeepsService.js"
import { logger } from "../utils/Logger.js";

class ProfilesService{
    async getProfile(id){
        const res = await api.get("api/profiles/"+id);
        
        AppState.profile = res.data;
        await keepsService.getKeepsByUserId(id);
        logger.log(AppState.profile);


    }
   


}

export const profilesService = new ProfilesService();