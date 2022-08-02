<template>
  <div class="component"></div>
  
</template>


<script>
import { computed, onMounted } from "@vue/runtime-core";
import { AppState } from "../AppState";
import { keepsService } from "../services/KeepsService";
import { Modal } from "bootstrap";
import Pop from "../utils/Pop";
import { useRoute, useRouter } from "vue-router";
import { vaultKeepsService } from "../services/VaultKeepsService";
import { vaultsService } from "../services/VaultsService";
import { router } from "../router";
export default {

  name: "Vault",
  setup() {
    const route = useRoute();
    const router = useRouter();
    onMounted(async () => {
      try {
        await vaultsService.getVaultById(route.params.id);
        await vaultsService.getVaultKeeps(route.params.id);
        // await keepsService.getVaultKeeps(route.params.id);

      }
      catch (error) {
        logger.error(error);
        Pop.toast(error.message);
        router.push({ name: "Home" });
      }
    });
    return {
      keeps: computed(() => AppState.keeps),
      activeVault: computed(() => AppState.activeVault),
      async deleteVaults() {
        try {
          await vaultsService.deleteVaults(AppState.activeVault);
          Pop.toast("Vault gone");
          router.push({
            name: "profile",
            params: {
              id: props.activeVault.userId
            }
          });
        } catch (error) {
          logger.error(error);
          Pop.toast(error.message);
        }
      },
    }
  }
}
</script>


<style lang="scss" scoped>
</style>