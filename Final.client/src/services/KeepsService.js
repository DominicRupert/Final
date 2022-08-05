import { AppState } from "../AppState.js";
import { logger } from "../utils/logger.js";
import { api } from "./AxiosService.js";
import { profilesService } from "../services/ProfilesService.js";


class KeepsService {
  async createKeep(newKeep) {
    try {
      const res = await api.post("api/keeps", newKeep);
      logger.log("[createKeep]", res.data);
      AppState.keeps.unshift(res.data);
    } catch (error) {
      logger.error(error);
    }
  }
  async getKeeps() {
    try {
      const res = await api.get("api/keeps");
      logger.log("[getKeeps]", res.data);
      AppState.keeps = res.data;
      // AppState.keeps.push(res.data); //
    } catch (error) {
      logger.error(error);
    }

  }
  async getKeepsByUserId(id) {

    const res = await api.get(`api/profiles/${id}/keeps`);
    logger.log("[getKeepsByUserId]", res.data);
    AppState.profileKeeps = res.data;
  }

  async getKeep(id) {
    try {
      const res = await api.get("api/keeps/" + id);
      logger.log("[getKeep]", res.data);
      AppState.activeKeep = res.data;
      // AppState
      
    } catch (error) {
      logger.error(error);
    }
  }

  async updateKeep(body) {
    try {
      const res = await api.put("api/keeps/" + body.id, body);
      logger.log("Keep updated", res.data);
      AppState.keeps.push = res.data;
    } catch (error) {
      logger.error(error);
    }
  }
  async deleteKeep(keepId) {
    try {
      const res = await api.delete("api/keeps/" + keepId);
      logger.log("[deleteKeep]", res.data);
      AppState.keeps.unshift = res.data;
     
 
    } catch (error) {
      logger.error(error);
    }
  }

  async setActive(keep) {
    AppState.activeKeep = keep;
    const res = await api.get(`api/keeps/${keep.id}`);
   
    AppState.activeKeep = res.data;
  }
  async addToVault(keepId,vaultId) {
    const vk = {vaultId,keepId}
    const res = await api.post(`api/vaultkeeps/`,vk);
    logger.log("[addToVault]", res.data);
    AppState.vaultKeeps.push(res.data);
    
    
  }
  async getKeepsByVaultId(id) {
    try {
      const res = await api.get(`api/vaults/${id}/keeps`);
      logger.log("[getKeepsByVaultId]", res.data);
      AppState.keeps = res.data;
    } catch (error) {
      logger.error(error);
    }
  }
}

export const keepsService = new KeepsService();
