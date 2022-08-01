import { AppState } from "../AppState.js";
import { logger } from "../utils/logger.js";
import { api } from "./AxiosService.js";



class KeepsService{

    async createKeep(body){
        try{
            const res = await api.post('api/keeps',body);
            AppState.keeps.push(res.data);
        }catch(error){
            logger.error(error);
        }
    }

    async getKeeps(query = ''){
        try{
            const res = await api.get('api/keeps'+query);
            logger.log('[getKeeps]',res.data);
            AppState.keeps=res.data;
        }catch(error){
            logger.error(error);
        }
    }
    async getKeepsByProfile(profileId){
        try{
            const res = await api.get(`api/profiles/${profileId}/keeps`);
            AppState.profileKeeps = res.data;
        }catch(error){
            logger.error(error);
        }
    }

    async getKeep(id){
        try{
            const res = await api.get('api/keeps/'+id);
            logger.log('[getKeep]',res.data);
            AppState.activeKeep=res.data;
        }catch(error){
            logger.error(error);
        }
    }

    async updateKeep(body){
        try{
            const res = await api.put('api/keeps/'+body.id,body);
            logger.log('Keep updated',res.data);
            AppState.keeps=res.data;
        }catch(error){
            logger.error(error);
        }
    }
    async deleteKeep(keepId){
        try{
            const res = await api.delete('api/keeps/'+keepId);
            logger.log('Keep deleted',res.data);
            AppState.accountKeeps=AppState.accountKeeps.filter(keep=>keep.id!==keepId);
        }catch(error){
            logger.error(error);
        }
    }

    async setActive(keep){
        AppState.activeKeep=keep;
        const res = await api.get(`api/keeps/${keep.id}`);
        AppState.activeKeep=res.data;
    }

}


export const keepsService = new KeepsService();