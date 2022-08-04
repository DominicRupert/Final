import { AppState } from "../AppState.js";
import { api } from "./AxiosService.js";
import { profilesService } from "../services/ProfilesService.js";
import { keepsService } from "./KeepsService.js";
import { logger } from "../utils/Logger.js";

class VaultKeepsService {
  async getVaultKeeps(vaultId) {
    
    const res = await api.get(`api/vaults/${vaultId}/keeps`);

    logger.log("[getVaultKeeps]", res.data);
    AppState.vaultKeeps = res.data;  
  }
  async getVaultKeepsByVaultId(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`);
    logger.log("[getVaultKeepsByVaultId]", res.data);
    AppState.vaultKeeps = res.data;
  }

    async createVaultKeeps(body) {
      const res = await api.post("api/vaultkeeps", body);
      logger.log("[createVaultKeeps]", res.data);
      AppState.vaultKeeps.push(res.data);
     
    }
    // async deleteVaultKeeps(keepId, vaultId) {
    //   const vk = {vaultId,keepId}
    //     const res = await api.delete(`api/vaultkeeps/` + vk);
    //     logger.log("[deleteVaultKeeps]", res.data);
    //     AppState.vaultKeeps = res.data;
    // }
    async removeKeeps(keepId, vaultId) {
      const vk = {vaultId,keepId}
      const res = await api.delete(`api/vaultkeeps/` + vk);
      await this.getVaultKeeps(vaultId);
      logger.log("[removeKeeps]", res.data);
      AppState.vaultKeeps = res.data;
    
    }
    
  }


export const vaultKeepsService = new VaultKeepsService();