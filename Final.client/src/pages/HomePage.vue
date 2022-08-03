
<template>
        <KeepModal />



  <div class="container ">
    <NewVault/>
    <CreateKeep/>
    <div class="masonry-with-columns">

      <div v-for="k in keeps" :key="k.id" class=" p-3 ">
        <Keep :keep="k" />
    </div>
    </div>
    </div>

 
</template>

<script>
import {computed, onMounted, ref} from 'vue'
import {AppState} from '../AppState.js'
import { logger } from '../utils/Logger.js'
import { keepsService } from '../services/KeepsService.js'
import { profilesService } from '../services/ProfilesService.js'
import { useRoute } from 'vue-router'
import  Pop  from '../utils/Pop.js'
import { vaultsService } from '../services/VaultsService.js'
import { vaultKeepsService } from '../services/VaultKeepsService.js'
import { accountService } from '../services/AccountService.js'
import { Modal } from 'bootstrap'
import NewVault from '../components/NewVault.vue'
import CreateKeep from '../components/CreateKeep.vue'
export default {
    props: {
        keep: {
            type: Object,
            required: true
        }
    },
    props: {
        vault: {
            type: Object,
            required: true
        }
    },
    name: "Home",
    setup(props) {
        onMounted(async () => {
            try {
                await vaultsService.getVaultsByUserId(AppState.vaults.userId);
                await keepsService.getKeeps();
            }
            catch (e) {
                logger.error(e);
                Pop.toast(e.message);
            }
        });
        return {
            async setActive() {
                try {
                    await keepsService.setActive(props.keep);
                }
                catch (e) {
                    logger.error(e);
                    Pop.toast(e.message);
                }
            },
            keeps: computed(() => AppState.keeps)
        };
    },
    components: { NewVault, CreateKeep }
}
</script>

<style scoped lang="scss">

.masonry-with-columns {
  margin-top: 20px;
  margin-bottom: 20px;
  display: grid;
  padding: 2em;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  grid-auto-rows: minmax(100px, auto);
}

.home{
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;
  .home-card{
    width: 50vw;
    > img{
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
